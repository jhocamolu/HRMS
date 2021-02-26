using Reportes.Support;
using Reportes.Support.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Reportes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggingConfig.ConfigureLogger("Api Reportes");
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
