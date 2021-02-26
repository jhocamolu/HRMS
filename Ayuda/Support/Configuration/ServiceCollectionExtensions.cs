using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using Ayuda.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Newtonsoft.Json;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Net.Http.Headers;
using System.Linq;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;

namespace Ayuda.Support.Configuration
{

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<AyudaDbContext>(
               opt => opt.UseSqlServer(configuration.GetConnectionString(Constants.ApplicationNameConnection.Default))
               );
            return services;
        }

        /// <summary>
        /// Configuracion de Mediatr
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            return services;
        }


        /// <summary>
        /// Configuracion CORS
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });


            });

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddFluentValidation()

                  ;

            return services;
        }

        public static IServiceCollection AddCustomMvcCore(this IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
            return services;
        }
    }
}
