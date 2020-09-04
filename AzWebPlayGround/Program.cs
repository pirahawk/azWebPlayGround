using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AzWebPlayGround
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, configBuilder) =>
                {
                    var currentDirectory = Environment.CurrentDirectory;
                    var azSettingsPath = Directory.GetFiles(currentDirectory, "azSettings.json").FirstOrDefault();
                    
                    if (string.IsNullOrWhiteSpace(azSettingsPath))
                    {
                        return;
                    }

                    configBuilder.AddJsonFile(azSettingsPath);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
