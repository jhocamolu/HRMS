using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadCentroCostos.comandos.Eliminar
{
    public class EliminarActividadCentroCostoHandler : IRequestHandler<EliminarActividadCentroCostoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarActividadCentroCostoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarActividadCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadCentroCosto actividadCentroCosto = contexto.ActividadCentroCostos.Find(request.Id);
                if (actividadCentroCosto == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.ActividadCentroCostos.Remove(actividadCentroCosto);
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
