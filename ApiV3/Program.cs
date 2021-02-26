using ApiV3.Support;
using ApiV3.Support.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ApiV3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggingConfig.ConfigureLogger(Constants.ApplicationName);
            Log.Logger.Information("Logger configured for IntegrationService");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
