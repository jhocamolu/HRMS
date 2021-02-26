using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CentroTrabajos.Comandos.Eliminar
{
    public class EliminarCentroTrabajoHandler : IRequestHandler<EliminarCentroTrabajoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCentroTrabajoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCentroTrabajoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CentroTrabajo centroTrabajo = await this.contexto.CentroTrabajos.FindAsync(request.Id);
                if (centroTrabajo == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.CentroTrabajos.Remove(centroTrabajo);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }
    }
}
