using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Support.Logging
{
    public class LoggingConfig
    {
        /// <summary>
        /// Configures serilog.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <returns>Serilog logger.</returns>
        public static Logger ConfigureLogger(string applicationName)
        {
            try 
            {
                var configuration = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("serilog.json", optional: false)
                                    //.AddJsonFile("serilog.Production.json", optional: true)
                                    .AddEnvironmentVariables()
                                    .Build();

                var logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(configuration)
                   .Enrich.WithProperty("ApplicationName", applicationName)
                   .Enrich.WithMachineName()
                   .Enrich.WithEnvironmentUserName()
                   .CreateLogger();

                return logger;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
