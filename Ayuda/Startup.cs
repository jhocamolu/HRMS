using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ayuda.Models;
using Ayuda.Support.Configuration;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;

namespace Ayuda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddODataQueryFilter();
            services.AddCustomMvc();
            services.AddCustomContext(Configuration);
            services.AddCustomCors();
            services.AddCustomMediatR();
            services.AddHttpClient();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(builderCors =>
            {
                builderCors.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(config =>
            {
                config.MapODataServiceRoute("odata", "odata", GetEdmModel(app.ApplicationServices));
                config.EnableDependencyInjection();
            });


        }

        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EnableLowerCamelCase();
            builder.EntitySet<Articulo>("Articulos")
               .EntityType
               .Filter()
               .Count()
               .Expand()
               .OrderBy()
               .Page()
               .Select();
            builder.EntitySet<Categoria>("Categorias")
              .EntityType
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();
            builder.EntitySet<Clave>("Claves")
              .EntityType
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();


            return builder.GetEdmModel();
        }
    }
}
