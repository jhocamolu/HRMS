using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoPresupuestos.Comandos.Actualizar
{
    public class ActualizarCargoPresupuestoHandler : IRequestHandler<ActualizarCargoPresupuestoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarCargoPresupuestoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarCargoPresupuestoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoPresupuesto cargoPresupuesto = contexto.CargoPresupuestos.Find(request.Id);
                cargoPresupuesto.CargoId = (int)request.CargoId;
                cargoPresupuesto.AnnoVigenciaId = (int)request.AnnoVigenciaId;
                cargoPresupuesto.Cantidad = (int)request.Cantidad;

                this.contexto.CargoPresupuestos.Update(cargoPresupuesto);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(cargoPresupuesto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
