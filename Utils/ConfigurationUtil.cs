using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365.PacUtil.Utils
{
    internal static class ConfigurationUtil
    {
        public static IConfigurationRoot Config { get; set; }
        public static IConfigurationBuilder ConfigurationBuilder { get; set; }

        private const string SectionName = "Office365IpUrlWebService";

        public static void InitializeConfiguration()
        {
            ConfigurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets("c4395102-6ff0-466f-b2d8-58345cb88860", reloadOnChange: true)
                .AddEnvironmentVariables();

            Config = ConfigurationBuilder.Build();
        }

        public static string WebServiceRootUrl
        {
            get { return Config[$"{SectionName}:WebServiceRootUrl"]; }
        }

        public static string VersionPath
        {
            get { return Config[$"{SectionName}:VersionPath"]; }
        }

        public static string DataPath
        {
            get { return Config[$"{SectionName}:DataPath"]; }
        }

        public static string Instance
        {
            get { return Config[$"{SectionName}:Instance"]; }
        }

        public static bool IncludeIPv6
        {
            get { return bool.Parse(Config[$"{SectionName}:IncludeIPv6"]); }
        }

        public static string ClientRequestId
        {
            get { return Config[$"{SectionName}:ClientRequestId"]; }
        }

        public static string TemplateFileTokenStartMarker
        {
            get { return Config[$"{SectionName}:TemplateFileTokenStartMarker"]; }
        }

        public static string TemplateFileTokenEndMarker
        {
            get { return Config[$"{SectionName}:TemplateFileTokenEndMarker"]; }
        }

        public static string OutputPath
        {
            get { return Config[$"{SectionName}:OutputPath"]; }
        }

        public static bool OverrideOutputPathWithUserTempPath
        {
            get { return bool.Parse(Config[$"{SectionName}:OverrideOutputPathWithUserTempPath"]); }
        }
    }
}
