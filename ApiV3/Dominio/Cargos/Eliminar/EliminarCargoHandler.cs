using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Cargos.Eliminar
{
    public class EliminarCargoHandler : IRequestHandler<EliminarCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Cargo cargo = this.contexto.Cargos.Find(request.Id);
                if (cargo == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.Cargos.Remove(cargo);
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
