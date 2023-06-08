using AntropoPollWebApi.Core.Services;
using AntropoPollWebApi.Core.Settings;
using AntropoPollWorker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AntropoPollWorker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("AntropoWorker starting...");
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AntropoPollSettings>(hostContext.Configuration.GetSection("AntropoPollSettings"));
                    services.AddTransient<CalcService>();
                    services.AddHostedService<WorkerService>();

                })
                .ConfigureLogging(logging =>
                {
                   // logging.ClearProviders();
                   // logging.SetMinimumLevel(LogLevel.Trace);
                });
        }

    }
}

