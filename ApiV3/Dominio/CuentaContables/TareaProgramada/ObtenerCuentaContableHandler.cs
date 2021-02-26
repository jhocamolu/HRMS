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

namespace ApiV3.Dominio.CuentaContables.TareaProgramada
{
    public class ObtenerCuentaContableHandler : IRequestHandler<ObtenerCuentaContableRequest, CommandResult>
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        protected IConfiguration configuration;
        protected IPeticionService peticion;
        private readonly NominaDbContext contexto;

        public ObtenerCuentaContableHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPeticionService peticion)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.peticion = peticion;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerCuentaContableRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                var host = this.configuration.GetValue<string>(Constants.InformacionSoftlandSicon.CuentaContable);
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

                        NaturalezaContable naturalezaCuenta;
                        if (item.Value<String>("saldoNormal") == "D")
                        {
                            naturalezaCuenta = NaturalezaContable.Debito;
                        }
                        else
                        {
                            naturalezaCuenta = NaturalezaContable.Credito;
                        }

                        var cuentaContable = contexto.CuentaContables.FirstOrDefault(x => x.Cuenta == item.Value<String>("cuentaContable"));
                        if (cuentaContable != null)
                        {
                            //Actualiza el registro de la cuenta contable
                            bool actualiza = false;
                            if (cuentaContable.Nombre != item.Value<String>("descripcion"))
                            {
                                cuentaContable.Nombre = item.Value<String>("descripcion");
                                actualiza = true;
                            }
                            if (cuentaContable.Naturaleza != naturalezaCuenta)
                            {
                                cuentaContable.Naturaleza = naturalezaCuenta;
                                actualiza = true;
                            }

                            if (actualiza == true)
                            {
                                contexto.CuentaContables.Update(cuentaContable);
                                await contexto.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            //Crea una cuenta contable
                            var cuentaNueva = new CuentaContable();
                            cuentaNueva.Cuenta = item.Value<String>("cuentaContable");
                            cuentaNueva.Nombre = item.Value<String>("descripcion");
                            cuentaNueva.Naturaleza = naturalezaCuenta;
                            cuentaNueva.EstadoRegistro = EstadoRegistro.Activo;
                            contexto.CuentaContables.Add(cuentaNueva);
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
