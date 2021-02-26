using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Embargos.Comandos.Eliminar
{
    public class EliminarEmbargosHandler : IRequestHandler<EliminarEmbargosRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarEmbargosHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarEmbargosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Embargo embargo = await this.contexto.Embargos.FindAsync(request.Id);
                if (embargo == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }

                this.contexto.Embargos.Remove(embargo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
