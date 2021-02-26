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

namespace ApiV3.Dominio.ActividadFuncionarios.Comandos.Crear
{
    public class CrearActividadFuncionarioHandler : IRequestHandler<CrearActividadFuncionarioRequest, CommandResult>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        protected IConfiguration configuration;
        protected IPeticionService peticion;
        private readonly NominaDbContext contexto;

        public CrearActividadFuncionarioHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPeticionService peticion, NominaDbContext contexto)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.peticion = peticion;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearActividadFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                var host = this.configuration.GetValue<string>(Constants.InformacionSoftlandSicon.ActividadFuncionario);

                host += $"?FechaInicio={request.FechaInicio.Value.ToString("yyyy/MM/dd")}&FechaFin={request.FechaFin.Value.ToString("yyyy/MM/dd")}";

                HttpResponseMessage httpResponse = await this.peticion.Get(host);
                if (httpResponse.IsSuccessStatusCode)
                {
                    if ((int)httpResponse.StatusCode == 204)
                    {
                        return CommandResult.Success();
                    }

                    String content = await httpResponse.Content.ReadAsStringAsync();
                    JToken respuesta = JToken.Parse(content);

                    foreach (var item in respuesta)
                    {
                        if (!String.IsNullOrEmpty(item.Value<String>("cedula")) && !String.IsNullOrEmpty(item.Value<String>("municipioId")) && !String.IsNullOrEmpty(item.Value<String>("codigo")))
                        {
                            //Consulta informaíón del municipio
                            var municipio = contexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Codigo == item.Value<String>("municipioId") && x.EstadoRegistro == EstadoRegistro.Activo);
                            //Consulta información del funcionario
                            var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.NumeroDocumento == item.Value<String>("cedula") && x.EstadoRegistro == EstadoRegistro.Activo);
                            //Consulta informacón de la actividad
                            var actividad = contexto.Actividades.FirstOrDefault(x => x.Codigo == item.Value<String>("codigo") && x.EstadoRegistro == EstadoRegistro.Activo);

                            if (municipio != null && funcionario != null && actividad != null)
                            {
                                // Inserta en la tabla ActividadFuncionario;
                                var actividadFuncionario = new ActividadFuncionario();
                                actividadFuncionario.FuncionarioId = funcionario.Id;
                                actividadFuncionario.ActividadId = actividad.Id;
                                actividadFuncionario.MunicipioId = municipio.Id;
                                actividadFuncionario.FechaInicio = (DateTime)request.FechaInicio;
                                actividadFuncionario.FechaFinalizacion = (DateTime)request.FechaFin;
                                actividadFuncionario.Cantidad = Int32.Parse(item.Value<String>("cantidad"));
                                actividadFuncionario.Estado = EstadoActividadFuncionario.Pendiente;
                                contexto.ActividadFuncionarios.Add(actividadFuncionario);
                                await contexto.SaveChangesAsync();
                            }
                        }
                    }
                    dynamic mensajeRespuesta = new { };
                    return CommandResult.Success(mensajeRespuesta);
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
