using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Graficas;
using ApiV3.Infraestructura.Repositorio;
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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Dashboard.Comandos.GraficasWeb
{
    public class GraficasWebDashboardHandler : IRequestHandler<GraficasWebDashboardRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IGraficaServices graficas;
        private readonly IConfiguration configuration;
        private readonly IPeticionService peticion;

        public GraficasWebDashboardHandler(NominaDbContext contexto, IReadOnlyRepository repositorio, IGraficaServices grafica, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPeticionService peticionService)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
            this.graficas = grafica;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.peticion = peticionService;
        }

        public async Task<CommandResult> Handle(GraficasWebDashboardRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dictionary<string, object> objetos = new Dictionary<string, object>();
                Funcionario funcionario = InformacionToken.ObtenerInformacionFuncionario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], contexto);

                JObject permisos = await ObtenerServicios(request);
                // devuelve error de funcionarios
                if (funcionario == null && permisos == null)
                {
                    return CommandResult.Fail("El funcionario no existe o no tiene permisos.", 401);
                }

                var listapermisosDashboard = permisos["permisos"];
                var dash = listapermisosDashboard.Where(c => c.ToString().StartsWith("Dashboard")).ToList();
                string envioPermisos = dash.Count > 0 ? $"{String.Join(",", dash)}" : "";
                var dashboard = contexto.Dashboards(funcionario.Id, envioPermisos);
                int contador = 1;
                int contadorColor = 1;
                Dictionary<int, string> colorWidget = this.ColorWidget();
                foreach (var item in dashboard)
                {
                    object tablero = null;
                    EnumTipoElemento tipo;
                    Enum.TryParse(item.Tipo, out tipo);
                    EnumSubtipoElemento subtipo;
                    Enum.TryParse(item.Subtipo, out subtipo);
                    switch (tipo)
                    {
                        case EnumTipoElemento.Tarjeta:
                            if (contadorColor > 16)
                            {
                                contadorColor = 1;
                            }
                            string color;
                            colorWidget.TryGetValue(contadorColor, out color);
                            contadorColor++;
                            tablero = graficas.Widget(item.Nombre, item.Cantidad.ToString(), color, item.Extra, item.Datos);
                            break;
                        case EnumTipoElemento.Grafica:
                            object label = null;
                            object data = null;
                            if (item.Datos != null)
                            {
                                JObject datos = JObject.Parse(item.Datos);
                                label = datos["Label"];
                                data = datos["Data"];
                            }
                            string tipoGrafica = subtipo == EnumSubtipoElemento.Barras ? "bar" : "doughnut";
                            tablero = graficas.GraficaBasica(tipoGrafica, item.Nombre, label, data, null, null, null);
                            break;
                        case EnumTipoElemento.Otro:
                            string url = this.configuration.GetValue<string>(Constants.ServiceNode.ARCHIVOS);
                            object otro;
                            if (subtipo == EnumSubtipoElemento.Cumpleanios)
                            {
                                List<object> listaCumpleanios = new List<object>();
                                if (item.Datos != null)
                                {
                                    JObject datos = JObject.Parse(item.Datos);
                                    var valor = datos["Funcionarios"];

                                    foreach (var adjunto in valor)
                                    {
                                        object llenar = new
                                        {
                                            foto = url + "/v1/bucket/download?document_id=" + adjunto["Adjunto"],
                                            nombre = adjunto["Nombre"],
                                            fecha = adjunto["Fecha"]
                                        };
                                        listaCumpleanios.Add(llenar);
                                    }
                                }
                                otro = new
                                {
                                    label = item.Nombre,
                                    datos = listaCumpleanios
                                };
                            }
                            else
                            {
                                otro = new
                                {
                                    label = item.Nombre,
                                    datos = item.Datos
                                };
                            }
                            tablero = otro;
                            break;
                        default:
                            break;
                    }

                    objetos.Add(item.Ubicacion + "-" + (contador++).ToString(), tablero);
                }
                return CommandResult.Success(objetos);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
        private async Task<JObject> ObtenerServicios(GraficasWebDashboardRequest request)
        {
            try
            {
                var url = configuration.GetValue<string>(Constants.Peticion.PERMISOAPLICACION);
                HttpResponseMessage httpResponse = await peticion.Post(url, request);
                if (!httpResponse.IsSuccessStatusCode) return null;
                return JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private Dictionary<int, string> ColorWidget()
        {
            Dictionary<int, string> color = new Dictionary<int, string>();
            color.Add(1, "#B72974");
            color.Add(2, "#FFA124");
            color.Add(3, "#066F77");
            color.Add(4, "#6232CC");
            color.Add(5, "#004693");
            color.Add(6, "#EE564C");
            color.Add(7, "#602411");
            color.Add(8, "#EF6100");
            color.Add(9, "#FF7D43");
            color.Add(10, "#8822A0");
            color.Add(11, "#3DBDD3");
            color.Add(12, "#CE7459");
            color.Add(13, "#9B193E");
            color.Add(14, "#3FD195");
            color.Add(15, "#FF7D7D");
            color.Add(16, "#9ABF00");
            return color;
        }
    }
}
