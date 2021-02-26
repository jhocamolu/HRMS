using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoSoportes.Comandos.Eliminar
{
    public class EliminarTipoSoporteHandler : IRequestHandler<EliminarTipoSoporteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoSoporteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoSoporteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoSoporte tipoSoporte = await this.contexto.TipoSoportes.FindAsync(request.Id);
                if (tipoSoporte == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoSoportes.Remove(tipoSoporte);
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
