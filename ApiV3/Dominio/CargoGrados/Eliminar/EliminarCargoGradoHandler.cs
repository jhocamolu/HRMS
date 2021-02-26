using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoGrados.Eliminar
{
    public class EliminarCargoGradoHandler : IRequestHandler<EliminarCargoGradoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCargoGradoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCargoGradoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoGrado cargoGrado = this.contexto.CargoGrados.Find(request.Id);
                if (cargoGrado == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.CargoGrados.Remove(cargoGrado);
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
