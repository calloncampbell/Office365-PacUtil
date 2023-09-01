using Office365.PacUtil.Models;
using Office365.PacUtil.Options;
using Office365.PacUtil.Services;
using Office365.PacUtil.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.NamingConventionBinder;
using System.CommandLine.Parsing;

namespace Office365.PacUtil
{
    public class Program
    {
        static async Task Main(string[] args)
        {
			try
			{
                ConfigurationUtil.InitializeConfiguration();

                await BuildCommandLine()
                    .UseHost(_ => Host.CreateDefaultBuilder(),
                        host =>
                        {
                            host.ConfigureServices(services =>
                            {
                                services.AddSingleton<IPacFileService, PacFileService>();
                            });
                        })
                    .UseDefaults()
                    .CancelOnProcessTermination()
                    .Build()
                    .InvokeAsync(args);
            }
			catch (Exception ex)
			{
				ConsoleUtil.WriteError(ex.Message, ex);
			}
        }

        /// <summary>
        /// Builds the command line.
        /// </summary>
        /// <returns></returns>
        public static CommandLineBuilder BuildCommandLine()
        {
            var root = new RootCommand("Proxy Auto Configuration (PAC) File Utility for Office 365.");

            // Command: pac-file
            var commandPacFile = new Command("pac-file", "Command to work with the Office 365 IP and Url web service");

            // Sub-Command: generate
            Command commandPacFileUpdate = new Command("generate", "Generate the PAC File from template file.")
            {
                new Option<FileInfo>(new []{"--file", "-f"}, "The PAC file template to be used in file generation.")
                {
                    IsRequired = true,
                }
            };
            commandPacFileUpdate.Handler = CommandHandler.Create<PacFileOptions, IHost, CancellationToken>(PacCommandFileUpdate);
            commandPacFile.AddCommand(commandPacFileUpdate);

            // Sub-Command: status
            Command commandPacFileStatus = new Command("update-check", "Checks for updates from Microsoft.");
            commandPacFileStatus.Handler = CommandHandler.Create<PacFileOptions, IHost, CancellationToken   >(PacCommandStatus);
            commandPacFile.AddCommand(commandPacFileStatus);

            root.AddCommand(commandPacFile);

            return new CommandLineBuilder(root);
        }

        /// <summary>
        /// Creates the PAC File.
        /// </summary>
        /// <param name="host"></param>
        private static async Task<int> PacCommandFileUpdate(PacFileOptions options, IHost host, CancellationToken token)
        {
            var serviceProvider = host.Services;
            var service = serviceProvider.GetRequiredService<IPacFileService>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Program));

            try
            {
                options.Action = PacFileActions.Generate;
                logger.LogInformation(LogEvents.PacFileEvent, $"Pac file action was requested for '{options.Action}'.");
                await service.PacFileActionAsync(options, token);
                return 0;
            }
            catch (OperationCanceledException) 
            {
                return 1;
            }
        }

        /// <summary>
        /// Displays the status of ...
        /// </summary>
        /// <param name="host"></param>
        private static async Task<int> PacCommandStatus(PacFileOptions options, IHost host, CancellationToken token)
        {
            var serviceProvider = host.Services;
            var service = serviceProvider.GetRequiredService<IPacFileService>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Program));

            try
            {
                options.Action = PacFileActions.UpdateCheck;
                logger.LogInformation(LogEvents.PacFileEvent, $"Pac file action was requested for '{options.Action}'");
                await service.PacFileActionAsync(options, token);
                return 0;
            }
            catch (OperationCanceledException)
            {
                return 1;
            }
        }
    }
}