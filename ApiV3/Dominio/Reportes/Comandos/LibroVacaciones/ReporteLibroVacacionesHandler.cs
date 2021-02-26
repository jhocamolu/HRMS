using ApiV3.Infraestructura.Resultados;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Reportes.Comandos.LibroVacaciones
{
    public class ReporteLibroVacacionesHandler : Peticion, IRequestHandler<ReporteLibroVacacionesRequest, CommandResult>
    {
        const string ALIAS = "LibroVacaciones";

        public ReporteLibroVacacionesHandler(IConfiguration configuration, IPeticionService peticion)
        {
            this.configuration = configuration;
            this.peticion = peticion;
        }

        public async Task<CommandResult> Handle(ReporteLibroVacacionesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var host = this.configuration.GetValue<string>(Constants.ServiceApi.REPORTES);

                JObject data = await this.ObtenerServicios($"{host}/api/Reportes/{ALIAS}");
                if (data == null) return CommandResult.Fail("Error al conectar con el servicio.", 500);

                if (!data.ContainsKey("alias"))
                {
                    if (data["alias"] == null)
                    {
                        return CommandResult.Fail("El reporte que intentas generar no existe.", 400);
                    }
                }

                Dictionary<string, dynamic> post = new Dictionary<string, dynamic>();

                post.Add("reporteId", data["id"].ToString());
                object parametros = new
                {
                    Periodo = request.Periodo,
                    EstadoContrato = request.EstadoContrato,
                    CentroOperativoId = request.CentroOperativoId,
                    DependenciaId = request.DependenciaId,
                    FuncionarioId = request.FuncionarioId
                };
                post.Add("parametros", parametros);

                var list = await this.EnviarServicios($"{host}/api/generarReportes", post);
                if (list == null)
                {
                    return CommandResult.Fail("No se ha generado el reporte.");
                }

                return CommandResult.Success(list);
            }
            catch (Exception e)
            {

                return CommandResult.Fail(e.Message);
            }
        }
    }

}
