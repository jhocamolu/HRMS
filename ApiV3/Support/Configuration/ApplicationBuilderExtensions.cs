using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace ApiV3.Support.Configuration
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


        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app)
        {
            app.UseMvc(config =>
            {
                config.MapODataServiceRoute("odata", "odata", OdataEdm.GetEdmModel(app.ApplicationServices));
                config.EnableDependencyInjection();
            });

            Log.Logger.Information("mvc ready");
            return app;
        }
    }
}
