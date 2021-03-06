﻿using System;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using NLog;

namespace DotNet.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Insights Demo");
            Console.WriteLine("=========================");
            Console.WriteLine();

            var telemetryClient = new TelemetryClient();
            using (Program.GetApplicationInsightsModule())
            {
                while (true)
                {
                    Console.WriteLine("<1>: Buy");
                    Console.WriteLine("<2>: Sell");
                    Console.WriteLine("<3>: Exit");
                    Console.WriteLine();
                    Console.Write("Please select a command to execute: ");
                    try
                    {
                        Enum.TryParse(Console.ReadLine(), out Command command);
                        Program.ExecuteCommand(command, telemetryClient);
                    }
                    catch (Exception e)
                    {
                        telemetryClient.TrackException(e);
                        Console.WriteLine(e);
                        Console.WriteLine();
                    }
                    finally
                    {
                        telemetryClient.Flush();
                    }
                }
            }
        }

        private static void ExecuteCommand(Command command, TelemetryClient telemetryClient)
        {
            switch (command)
            {
                case Command.Buy:
                    Program.Buy();
                    break;

                case Command.Sell:
                    Program.Sell();
                    break;

                case Command.Close:
                    telemetryClient.Flush();
                    Console.WriteLine("Press any key to close the app.");
                    Console.ReadLine();
                    Task.Delay(2000).Wait();
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("No command has been executed.");
                    break;
            }
            
            Console.WriteLine();
        }

        private static DependencyTrackingTelemetryModule GetApplicationInsightsModule()
        {
            TelemetryConfiguration.Active.InstrumentationKey = "";

            var module = new DependencyTrackingTelemetryModule();

            // prevent Correlation Id to be sent to certain endpoints. You may add other domains as needed.
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.windows.net");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("localhost");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("127.0.0.1");
            //...

            // enable known dependency tracking, note that in future versions, we will extend this list. 
            // please check default settings in https://github.com/Microsoft/ApplicationInsights-dotnet-server/blob/develop/Src/DependencyCollector/NuGet/ApplicationInsights.config.install.xdt#L20
            module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.ServiceBus");
            module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.EventHubs");
            //....

            // initialize the module
            module.Initialize(TelemetryConfiguration.Active);

            // stamps telemetry with correlation identifiers
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());

            // ensures proper DependencyTelemetry.Type is set for Azure RESTful API calls
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

            return module;
        }

        private static void Buy()
        {
            throw new Exception("The Buy command has failed.");
        }

        private static void Sell()
        {
            Console.WriteLine("Test NLog.");

            var logger = LogManager.GetLogger("ApplicationInsightsDemo");

            logger.Trace("Using nLog to write a 'trace' message");
            logger.Debug("Using nLog to write a 'debug' message");
            logger.Info("Using nLog to write an 'info' message");
            logger.Warn("Using nLog to write a 'warning' message");
            logger.Error("Using nLog to write an 'error' message");
            logger.Fatal("Using nLog to write a 'fatal' message");

            throw new Exception("The Buy command has failed.");
        }
    }
}
