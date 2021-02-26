using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoPresupuestos.Comandos.Crear
{
    public class CrearCargoPresupuestoHandler : IRequestHandler<CrearCargoPresupuestoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCargoPresupuestoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCargoPresupuestoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoPresupuesto cargoPresupuesto = new CargoPresupuesto();
                cargoPresupuesto.CargoId = (int)request.CargoId;
                cargoPresupuesto.AnnoVigenciaId = (int)request.AnnoVigenciaId;
                cargoPresupuesto.Cantidad = (int)request.Cantidad;

                this.contexto.CargoPresupuestos.Add(cargoPresupuesto);
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
