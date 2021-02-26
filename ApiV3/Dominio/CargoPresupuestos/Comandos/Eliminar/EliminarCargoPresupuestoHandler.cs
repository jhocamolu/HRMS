using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoPresupuestos.Comandos.Eliminar
{
    public class EliminarCargoPresupuestoHandler : IRequestHandler<EliminarCargoPresupuestoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCargoPresupuestoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCargoPresupuestoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoPresupuesto cargoPresupuesto = this.contexto.CargoPresupuestos.Find(request.Id);
                if (cargoPresupuesto == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.CargoPresupuestos.Remove(cargoPresupuesto);
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
