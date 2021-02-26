using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ayuda.Support;
using Ayuda.Support.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Ayuda
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
