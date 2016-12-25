using KQAnalytics3.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace KQAnalytics3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Run KQ Configurator
            var currentDir = Directory.GetCurrentDirectory();
            KQRegistry.CurrentDirectory = currentDir;
            KQConfigurationAggregator.Initialize();
            KQConfigurationAggregator.LoadConfigurationFile();
            KQConfigurationAggregator.Reload();

            // Start application
            var config = new ConfigurationBuilder()
                .SetBasePath(currentDir)
                .AddCommandLine(args)
                .AddJsonFile(Path.Combine(currentDir, "hosting.json"), false)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(currentDir)
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}