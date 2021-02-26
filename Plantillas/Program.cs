using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Plantillas.Support;
using Plantillas.Support.Logging;
using Serilog;

namespace Plantillas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = LoggingConfig.ConfigureLogger(Constants.ApplicationName);
            Log.Logger.Information("Logger configured for IntegrationService");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
