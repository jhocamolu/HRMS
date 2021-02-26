using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.PeriodoContables.TareaProgramada
{
    public class ObtenerPeriodoContableHandler : IRequestHandler<ObtenerPeriodoContableRequest, CommandResult>
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        protected IConfiguration configuration;
        protected IPeticionService peticion;
        private readonly NominaDbContext contexto;

        public ObtenerPeriodoContableHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPeticionService peticion, NominaDbContext contexto)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.peticion = peticion;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerPeriodoContableRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                var host = this.configuration.GetValue<string>(Constants.InformacionSoftlandSicon.PeriodoContable);
                if (!String.IsNullOrEmpty(request.Fecha))
                {
                    host += "?FechaRegistroDesde=" + request.Fecha;
                }

                HttpResponseMessage httpResponse = await this.peticion.Get(host);
                if (httpResponse.IsSuccessStatusCode)
                {
                    if ((int)httpResponse.StatusCode == 204)
                    {
                        return CommandResult.Fail(httpResponse.ReasonPhrase, (int)httpResponse.StatusCode);
                    }
                    String content = await httpResponse.Content.ReadAsStringAsync();

                    JToken respuesta = JToken.Parse(content);

                    foreach (var item in respuesta)
                    {
                        if (item.Value<String>("contabilidad") == "C")
                        {
                            EstadoPeriodoContable estado;
                            if (item.Value<String>("estado") == "A")
                            {
                                estado = EstadoPeriodoContable.Activo;
                            }
                            else
                            {
                                estado = EstadoPeriodoContable.Cerrado;
                            }
                            var fechaFinal = item.Value<DateTime>("fechaFinal");
                            var periodoContable = contexto.PeriodoContables.FirstOrDefault(x => x.Fecha == fechaFinal.Date);
                            if (periodoContable != null)
                            {
                                //Verifica si el registro tiene cambios para realizar la actualización.
                                bool actualiza = false;
                                if (periodoContable.Nombre != item.Value<String>("descripcion"))
                                {
                                    periodoContable.Nombre = item.Value<String>("descripcion");
                                    actualiza = true;
                                }
                                if (periodoContable.Fecha != fechaFinal)
                                {
                                    periodoContable.Fecha = fechaFinal;
                                    actualiza = true;
                                }
                                if (periodoContable.Estado != estado)
                                {
                                    periodoContable.Estado = estado;
                                    actualiza = true;
                                }
                                if (actualiza == true)
                                {
                                    contexto.PeriodoContables.Update(periodoContable);
                                    await contexto.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                //Crea un centro de costo
                                var periodoContableNuevo = new PeriodoContable();
                                periodoContableNuevo.Fecha = fechaFinal;
                                periodoContableNuevo.Nombre = item.Value<String>("descripcion");
                                periodoContableNuevo.Estado = estado;
                                periodoContableNuevo.EstadoRegistro = EstadoRegistro.Activo;
                                contexto.PeriodoContables.Add(periodoContableNuevo);
                                await contexto.SaveChangesAsync();
                            }
                        }
                    }
                    return CommandResult.Success(respuesta);
                }
                return CommandResult.Fail(httpResponse.ReasonPhrase, (int)httpResponse.StatusCode);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

    }
}
