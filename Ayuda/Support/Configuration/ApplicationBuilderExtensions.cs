using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Support.Configuration
{
    /// <summary>
    /// Extensions methods for application builder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors();

            Log.Logger.Information("Cors ready");
            return app;
        }






    }
}
