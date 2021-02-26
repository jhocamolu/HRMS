using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NivelCargos.Comandos.Eliminar
{
    public class EliminarNivelCargoHandler : IRequestHandler<EliminarNivelCargoRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public EliminarNivelCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNivelCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NivelCargo nivelCargo = await this.contexto.NivelCargos.FindAsync(request.Id);
                if (nivelCargo == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.NivelCargos.Remove(nivelCargo);
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
