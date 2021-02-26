using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoEmbargos.Comandos.Eliminar
{
    public class EliminarTipoEmbargoHandler : IRequestHandler<EliminarTipoEmbargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoEmbargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoEmbargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoEmbargo tipoEmbargo = await this.contexto.TipoEmbargos.FindAsync(request.Id);
                if (tipoEmbargo == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoEmbargos.Remove(tipoEmbargo);
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
