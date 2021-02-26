using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoReportas.Crear
{
    public class CrearCargoReportaHandler : IRequestHandler<CrearCargoReportaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCargoReportaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCargoReportaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoReporta cargoReporta = new CargoReporta
                {
                    CargoDependenciaId = (int)request.CargoDependenciaId,
                    CargoDependenciaReportaId = (int)request.CargoDependenciaReportaId,
                    JefeInmediato = (bool)request.JefeInmediato
                };

                this.contexto.CargoReportas.Add(cargoReporta);
                await this.contexto.SaveChangesAsync();

                if (request.CargoDependenciaReportaId != null)
                {
                    cargoReporta.CargoDependenciaReporta.MeReportan = new List<CargoReporta>();
                }

                return CommandResult.Success(cargoReporta);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
