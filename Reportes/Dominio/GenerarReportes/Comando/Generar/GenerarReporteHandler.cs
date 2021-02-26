using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reportes.Infraestructura.DbContexto;
using Reportes.Infraestructura.Interface;
using Reportes.Infraestructura.Resultados;
using Reportes.Support;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reportes.Dominio.GenerarReportes.Comando.Generar
{
    public class GenerarReporteHandler : IRequestHandler<GenerarReporteRequest, CommandResult>
    {
        private readonly ReportesDbContext contexto;
        private readonly IReportService report;
        private IConfiguration configuration;
        private string host;

        public GenerarReporteHandler(ReportesDbContext contexto, IReportService report,  IConfiguration configuration)
        {
            this.contexto = contexto;
            this.report = report;
            this.configuration = configuration;
            this.host = this.configuration.GetValue<string>(Constants.UriEnviroment);
        }
        /// <summary>
        /// Recibe el request para validaciones
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna objecto de rute de archivo</returns>
        public async Task<CommandResult> Handle(GenerarReporteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Models.Reporte reporte = await contexto.Reportes.FirstOrDefaultAsync(x => x.Id == request.ReporteId);
                var content = await report.Ruta(reporte, request.Parametros.Any() ? request.Parametros : null, null);

                object ruta = new
                {
                    url = this.host,
                    file =  "/public/" + content
                };
                return CommandResult.Success(ruta);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 500);
            }
        }
    }
}
