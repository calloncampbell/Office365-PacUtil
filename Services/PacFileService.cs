using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Office365.PacUtil.Models;
using Office365.PacUtil.Options;
using Office365.PacUtil.Utils;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Office365.PacUtil.Services
{
    public class PacFileService : IPacFileService
    {
        private static HttpClient _httpClient = new HttpClient();

        public string LatestVersion { get; set; } = "0000000000";
        public const string OutputSubfolder = "PacUtil";

        public async Task<int> PacFileActionAsync(PacFileOptions options, CancellationToken token)
        {
            if (options.Action == PacFileActions.UpdateCheck)
            {
                await CheckForUpdatesAsync(token);
            }
            else if (options.Action == PacFileActions.Generate)
            {
                await CreatePacFileAsync(options, token);
            }
            else
            {
                ConsoleUtil.WriteWarning($"Pac file action '{options.Action}' is not supported.");
                return 1;
            }

            return 0;
        }

        private string GetOutputPath()
        {
            var path = string.Empty;
            if (ConfigurationUtil.OverrideOutputPathWithUserTempPath)
            {
                path = $"{Path.GetTempPath()}{OutputSubfolder}";
                return path;
            }

            path = $"{ConfigurationUtil.OutputPath}\\{OutputSubfolder}";           
            return path;
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationUtil.ClientRequestId))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'ClientRequestId'");
            }

            if (!string.IsNullOrWhiteSpace(ConfigurationUtil.ClientRequestId))
            {
                if (!Guid.TryParse(ConfigurationUtil.ClientRequestId, out var instanceGuid))
                {
                    throw new ConfigurationErrorsException("Invalid configuration value for 'ClientRequestId'. Please use a properly formatted GUID - xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx. The README.md file has more details and samples.");
                }
            }

            if (string.IsNullOrWhiteSpace(ConfigurationUtil.TemplateFileTokenStartMarker))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'TemplateFileTokenStartMarker'");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationUtil.TemplateFileTokenEndMarker))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'TemplateFileTokenEndMarker'");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationUtil.OutputPath))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'OutputPath'");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationUtil.WebServiceRootUrl))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'WebServiceRootUrl'");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationUtil.VersionPath))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'VersionPath'");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationUtil.DataPath))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'DataPath'");
            }

            if (string.IsNullOrWhiteSpace(ConfigurationUtil.Instance))
            {
                throw new ConfigurationErrorsException("Missing configuration for 'Instance'");
            }            
        }

        private async Task<bool> CheckForUpdatesAsync(CancellationToken token)
        {
            ConsoleUtil.WriteMessage("Checking for Office 365 IP Address and URL updates...");

            ValidateConfiguration();

            try
            {
                var url = $"{ConfigurationUtil.WebServiceRootUrl}/version/{ConfigurationUtil.Instance}/?clientrequestid={ConfigurationUtil.ClientRequestId}";

                ConsoleUtil.WriteMessage($"Making a REST API call to URL '{url}'...");

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var result = await _httpClient.SendAsync(request);
                var content = result?.Content == null ? null : await result.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new NullReferenceException("Office 365 Endpoint API data is null. Please check the configuration and try again.");
                }

                LatestVersion = JsonConvert.DeserializeObject<Office365VersionResult>(content).Latest;
                var currentVersionFile = $"{GetOutputPath()}\\{ConfigurationUtil.VersionPath}";
                if (File.Exists(currentVersionFile))
                {
                    var fileContents = await File.ReadAllTextAsync(currentVersionFile, token);
                    if (!String.Equals(content.Replace(" ", ""), fileContents.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase))
                    {

                        ConsoleUtil.WriteInfo($"A new version is available - '{LatestVersion}'.");
                        await File.WriteAllTextAsync(currentVersionFile, content, token);
                        return true;
                    }

                    ConsoleUtil.WriteInfo($"You already have the current version - '{LatestVersion}'.");
                    return false;
                }

                var path = Path.GetDirectoryName(currentVersionFile);
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException(nameof(path));
                }

                Directory.CreateDirectory(path);
                await File.WriteAllTextAsync(currentVersionFile, content, token);
                ConsoleUtil.WriteInfo($"The current version is '{LatestVersion}'.");
                return true;
            }
            catch (Exception ex)
            {
                ConsoleUtil.WriteError($"An exception occurred during update check. Details: {ex.Message}", ex);
                return false;
            }            
        }

        private async Task CreatePacFileAsync(PacFileOptions options, CancellationToken token)
        {
            ConsoleUtil.WriteMessage("Generating PAC file...");

            ValidateConfiguration();

            try
            {
                var updateAvailable = await CheckForUpdatesAsync(token);
#if !DEBUG
                if (!updateAvailable && !options.Force)
                {
                    return;
                }
#endif

                var url = $"{ConfigurationUtil.WebServiceRootUrl}/endpoints/{ConfigurationUtil.Instance}/?clientrequestid={ConfigurationUtil.ClientRequestId}";
                if (!ConfigurationUtil.IncludeIPv6)
                {
                    url = $"{url}&NoIPv6=$NoIpv6";
                }

                ConsoleUtil.WriteMessage("Downloading updated Office 365 IP Address and URLs...");

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var result = await _httpClient.SendAsync(request);
                var content = result?.Content == null ? null : await result.Content.ReadAsStringAsync();

                if (content != null)
                {
                    var currentDataFile = $"{GetOutputPath()}\\{ConfigurationUtil.DataPath}";
                    await File.WriteAllTextAsync(currentDataFile, content, token);

                    ConsoleUtil.WriteMessage("Processing data for Office 365 IP Address and URLs...");

                    var changesUrl = $"{ConfigurationUtil.WebServiceRootUrl}/changes/{ConfigurationUtil.Instance}/{LatestVersion}?clientrequestid={ConfigurationUtil.ClientRequestId}";

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(); 
                    sb.AppendLine($"// Microsoft 365 URLs and IP address data version: {LatestVersion}");
                    sb.AppendLine($"// URL to compare version {LatestVersion} with the latest: {changesUrl}");
                    sb.AppendLine();

                    var list = JsonConvert.DeserializeObject<JArray>(content);
                    
                    foreach (var item in list)
                    {
                        sb.AppendLine($"                // Event ID {item["id"]} - {item["serviceArea"]}");
                        
                        if (item["urls"] is not null)
                        {
                            foreach (var urlItem in item["urls"].ToArray())
                            {
                                sb.AppendLine($"                shExpMatch(host, \"{urlItem}\") ||");
                            }

                            sb.AppendLine();
                        }

                        if (item["ips"] is not null)
                        {
                            foreach (var ipItem in item["ips"].ToArray())
                            {
                                var ipRangeResult = NetworkUtil.CalculateIpV4Subnet(ipItem.ToString());
                                sb.AppendLine($"                isInNet(host, \"{ipRangeResult.Item1}\", \"{ipRangeResult.Item2}\") ||");
                            }

                            sb.AppendLine();
                            sb.AppendLine();
                        }
                    }

                    // Remove trailing || and empty space
                    var finalText = sb.ToString();
                    finalText.TrimEnd();
                    finalText = finalText.Remove(finalText.Length - 6, 6);
                    finalText = finalText + "\n";

                    // Define your markers
                    string startMarker = ConfigurationUtil.TemplateFileTokenStartMarker;
                    string endMarker = ConfigurationUtil.TemplateFileTokenEndMarker;

                    // Merge into the template
                    var templateFile = await File.ReadAllTextAsync($"{options.File.FullName}", token);

                    // Replace the old contents between the markes with the newly generated content
                    int startIndex = templateFile.IndexOf(startMarker) + startMarker.Length;
                    int endIndex = templateFile.IndexOf(endMarker, startIndex);
                    var outputFile = templateFile.Remove(startIndex, endIndex - startIndex).Insert(startIndex, finalText);

                    // Output generated file into temp PacUtil location
                    var outputPath = $"{GetOutputPath()}\\proxy-{LatestVersion}.pac";
                    await File.WriteAllTextAsync(outputPath, outputFile, token);
                    ConsoleUtil.WriteInfo($"Successfully generated new proxy PAC file: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                ConsoleUtil.WriteError($"An exception occurred when generating PAC file. Details: {ex.Message}", ex);
            }
        }
    }
}


