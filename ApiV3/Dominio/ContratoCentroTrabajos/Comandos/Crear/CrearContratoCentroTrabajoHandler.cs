using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoCentroTrabajos.Comandos.Crear
{
    public class CrearContratoCentroTrabajoHandler : IRequestHandler<CrearContratoCentroTrabajoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearContratoCentroTrabajoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearContratoCentroTrabajoRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var conCentroTrabajoActual = (from conCentroTrabajo in contexto.ContratoCentroTrabajos
                                       join centrotrabajo in contexto.CentroTrabajos on conCentroTrabajo.CentroTrabajoId equals centrotrabajo.Id
                                       where conCentroTrabajo.ContratoId == request.ContratoId
                                       && conCentroTrabajo.EstadoRegistro == EstadoRegistro.Activo
                                       && conCentroTrabajo.FechaFin == null
                                       select new { id = conCentroTrabajo.Id }).FirstOrDefault();

                if (conCentroTrabajoActual != null)
                {

                    // Se resta mes anterior
                    var fechaFinal = (DateTime)request.FechaFinal;
                    fechaFinal = fechaFinal.AddMonths(-1);
                    var diaFinal = DateTime.DaysInMonth(fechaFinal.Year, fechaFinal.Month);
                    var ultimoDiaMesAnterior = $"{fechaFinal.Year}-{fechaFinal.Month}-{diaFinal}";

                    var terminarContratoCentroTrabajo = contexto.ContratoCentroTrabajos.Find(conCentroTrabajoActual.id);
                    terminarContratoCentroTrabajo.FechaFin = DateTime.Parse(ultimoDiaMesAnterior);

                    contexto.ContratoCentroTrabajos.Update(terminarContratoCentroTrabajo);
                    await contexto.SaveChangesAsync();
                }

                ContratoCentroTrabajo contratoCentroTrabajos = new ContratoCentroTrabajo
                {
                    ContratoId = (int)request.ContratoId,
                    FechaInicio = (DateTime)request.FechaInicio,
                    CentroTrabajoId = (int)request.CentroTrabajoId,
                    Observacion = request.Observacion
                };

                contexto.ContratoCentroTrabajos.Add(contratoCentroTrabajos);
                await contexto.SaveChangesAsync();

                if (contratoCentroTrabajos.Contrato.ContratoCentroTrabajos != null)
                {
                    contratoCentroTrabajos.Contrato.ContratoCentroTrabajos = null;
                }

                return CommandResult.Success(contratoCentroTrabajos);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}