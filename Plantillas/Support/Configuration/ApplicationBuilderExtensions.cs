using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Plantillas.Support.Configuration
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
