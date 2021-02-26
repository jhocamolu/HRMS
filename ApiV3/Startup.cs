using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Graficas;
using ApiV3.Infraestructura.ProcedimientoDinamicos;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Servicios;
using ApiV3.Servicios.Autenticacion;
using ApiV3.Servicios.Peticion;
using ApiV3.Servicios.RequestData;
using ApiV3.Support.Configuration;
using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ApiV3
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
            services.AddScoped<JwtValidationActionFilter>();
            services.AddTransient<IAutenticacionService, AutenticacionService>();
            services.AddScoped<IRequestData, RequestData>();
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<MongoDbContext>(
                Configuration.GetSection(nameof(MongoDbContext)));

            services.AddSingleton<IMongoDbContext>(sp =>
                sp.GetRequiredService<IOptions<MongoDbContext>>().Value);
            services.AddSingleton<MongoService>();
            services.AddScoped<IPeticionService, PeticionService>();
            services.AddScoped<IReadOnlyRepository, EntityFrameworkReadOnlyRepository<NominaDbContext>>();
            services.AddScoped<IGraficaServices, GraficaServices>();
            services.AddScoped<IDynamicProcedure, ProcedimientoDinamico>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //
            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureCors();

            app.ConfigureMvc();
        }
    }
}
