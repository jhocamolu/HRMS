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

namespace ApiV3.Dominio.Reportes.Comandos.ReporteRegresoVacacionesAusentismos
{
    public class ReporteRegresoVacacionesAusentismosHandler : Peticion, IRequestHandler<ReporteRegresoVacacionesAusentismosRequest, CommandResult>
    {
        const string ALIAS = "RegresoVacacionesAusentismos";

        public ReporteRegresoVacacionesAusentismosHandler(IConfiguration configuration, IPeticionService peticion)
        {
            this.configuration = configuration;
            this.peticion = peticion;

        }

        public async Task<CommandResult> Handle(ReporteRegresoVacacionesAusentismosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var host = this.configuration.GetValue<string>(Constants.ServiceApi.REPORTES);
                // consultar si existe alias del reporte
                JObject data = await this.ObtenerServicios($"{host}/api/Reportes/{ALIAS}");
                if (data == null) return CommandResult.Fail("Error al conectar con el servicio.", 500);

                if (!data.ContainsKey("alias"))
                {
                    if (data["alias"] == null)
                    {
                        return CommandResult.Fail("El reporte que intentas generar no existe.", 400);
                    }
                }

                // creacion del elemento post para enviar tipo formato JSON  clave - valor
                Dictionary<string, dynamic> post = new Dictionary<string, dynamic>();

                // Id del reporte
                post.Add("reporteId", data["id"].ToString());

                // parametros del reporte
                object parametros = new
                {
                    EstadoFuncionario = request.EstadoFuncionario,
                    CentroOperativo = request.CentroOperativo,
                    Dependencia = request.Dependencia,
                    Cargo = request.Cargo
                };

                post.Add("parametros", parametros);


                //enviar y obtener ruta del reporte
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
