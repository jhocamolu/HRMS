using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoCentroTrabajos.Comandos.Actualizar
{
    public class ActualizarContratoCentroTrabajoHandler : IRequestHandler<ActualizarContratoCentroTrabajoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarContratoCentroTrabajoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarContratoCentroTrabajoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                
                var contratoCentroTrabajo = contexto.ContratoCentroTrabajos.Find(request.Id);
                contratoCentroTrabajo.CentroTrabajoId = (int)request.CentroTrabajoId;
                contratoCentroTrabajo.Observacion = request.Observacion;
                contratoCentroTrabajo.FechaInicio = (DateTime)request.FechaInicio;
                contexto.ContratoCentroTrabajos.Update(contratoCentroTrabajo);
                await contexto.SaveChangesAsync();

                if (contratoCentroTrabajo.Contrato.ContratoCentroTrabajos != null)
                {
                    contratoCentroTrabajo.Contrato.ContratoCentroTrabajos = null;
                }
                return CommandResult.Success(contratoCentroTrabajo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
