using AntropoPollWebApi.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace AntropoPollWorker.Services
{
    public class ConfigService
    {
        private static ServiceProvider InitConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environment != null)
            {
                builder.AddJsonFile($"appsettings.{environment}.json", true);
            }

            var config = builder.Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AntropoPollSettings>(c => config.GetSection("AntropoPollSettings").Bind(c))
                .BuildServiceProvider();

            return serviceProvider;
        }
        public static AntropoPollSettings GetOpcConfig()
        {
            var serviceProvider = InitConfig();

            var opcServerConfig = serviceProvider.GetService<IOptions<AntropoPollSettings>>().Value;
            return opcServerConfig;
        }

    }
}
