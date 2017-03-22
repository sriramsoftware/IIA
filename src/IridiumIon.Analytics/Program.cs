using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IridiumIon.Analytics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var currentDir = Directory.GetCurrentDirectory();

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