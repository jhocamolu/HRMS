using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoViviendas.Comandos.Eliminar
{
    public class EliminarTipoViviendasHandler : IRequestHandler<EliminarTipoViviendasRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoViviendasHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoViviendasRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoVivienda tipoVivienda = await this.contexto.TipoViviendas.FindAsync(request.Id);
                if (tipoVivienda == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoViviendas.Remove(tipoVivienda);
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
