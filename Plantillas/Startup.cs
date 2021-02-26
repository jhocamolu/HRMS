using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Plantillas.Infraestructura;
using Plantillas.Models;
using Plantillas.Support.Configuration;
using RazorLight;
using System;
using System.IO;
using System.Reflection;

namespace Plantillas
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
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IRazorLightEngine>(sp =>
            {
                var engine = new RazorLightEngineBuilder()
                    .UseFilesystemProject(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                    .UseMemoryCachingProvider()
                    .Build();
                return engine;
            });

            services.AddCustomContext(Configuration);
            services.AddCustomCors();
            services.AddCustomMediatR();
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPeticionService, PeticionService>();
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
            builder.EntitySet<Etiqueta>("Etiquetas")
              .EntityType
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();
            builder.EntitySet<ComplementoPlantilla>("ComplementoPlantillas")
               .EntityType
               .Filter()
               .Count()
               .Expand()
               .OrderBy()
               .Page()
               .Select();
            builder.EntitySet<Plantilla>("Plantillas")
              .EntityType
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();
            builder.EntitySet<Documento>("Documentos")
              .EntityType
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();
            builder.EntitySet<GrupoDocumento>("GrupoDocumentos")
              .EntityType
              .Filter()
              .Count()
              .Expand()
              .OrderBy()
              .Page()
              .Select();
            builder.EntitySet<GrupoDocumentoEtiqueta>("GrupoDocumentoEtiquetas")
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
