using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Models;
using Reclutamiento.Servicios.Peticion;
using Reclutamiento.Support.Configuration;
using System;
using System.Text;

namespace Reclutamiento
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
            services.AddCustomControllers();
            services.AddCustomContext(Configuration);
            services.AddCustomCors();
            services.AddCustomMediatR();
            services.AddMediatR(typeof(Startup));
            services.AddDirectoryBrowser();
            services.AddIdentity<UsuarioAplicacion, IdentityRole>()
                    .AddEntityFrameworkStores<ReclutamientoDbContext>()
                    .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                         ClockSkew = TimeSpan.Zero
                     });
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPeticionService, PeticionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureCors();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
