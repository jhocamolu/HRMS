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

namespace ApiV3.Dominio.CentroCostos.TareaProgramada
{
    public class ObtenerCentroCostoHandler : IRequestHandler<ObtenerCentroCostoRequest, CommandResult>
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        protected IConfiguration configuration;
        protected IPeticionService peticion;
        private readonly NominaDbContext contexto;

        public ObtenerCentroCostoHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPeticionService peticion, NominaDbContext contexto)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.peticion = peticion;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                var host = this.configuration.GetValue<string>(Constants.InformacionSoftlandSicon.CentroCosto);
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
                        EstadoRegistro estadoRegistro;
                        if (item.Value<String>("tipo") == "A")
                        {
                            estadoRegistro = EstadoRegistro.Activo;
                        }
                        else
                        {
                            estadoRegistro = EstadoRegistro.Inactivo;
                        }

                        var centroCosto = contexto.CentroCostos.FirstOrDefault(x => x.Codigo == item.Value<String>("centroCosto"));
                        if (centroCosto != null)
                        {
                            //Verifica si el registro tiene cambios para realizar la actualización.
                            bool actualiza = false;
                            if (centroCosto.Nombre != item.Value<String>("descripcion"))
                            {
                                centroCosto.Nombre = item.Value<String>("descripcion");
                                actualiza = true;
                            }
                            if (centroCosto.EstadoRegistro != estadoRegistro)
                            {
                                centroCosto.EstadoRegistro = estadoRegistro;
                                actualiza = true;
                            }
                            if (actualiza == true)
                            {
                                contexto.CentroCostos.Update(centroCosto);
                                await contexto.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            //Crea un centro de costo
                            var centroNuevo = new CentroCosto();
                            centroNuevo.Codigo = item.Value<String>("centroCosto");
                            centroNuevo.Nombre = item.Value<String>("descripcion");
                            centroNuevo.EstadoRegistro = EstadoRegistro.Activo;
                            contexto.CentroCostos.Add(centroNuevo);
                            await contexto.SaveChangesAsync();
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
