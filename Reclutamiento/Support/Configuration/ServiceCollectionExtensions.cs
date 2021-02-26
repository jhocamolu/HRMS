using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Reclutamiento.Infraestructura.DbContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Support.Configuration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomContext(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddEntityFrameworkSqlServer().AddDbContext<ReclutamientoDbContext>(
               opt => opt.UseSqlServer(configuration.GetConnectionString(Constants.ApplicationNameConnection))
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
                options.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin());
            });

            return services;
        }

       public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.MaxIAsyncEnumerableBufferLimit = 2000000;
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(options =>
            {

                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
          
            return services;
        }
    }
}
