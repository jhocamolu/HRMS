using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoMonedas.Comandos.Eliminar
{
    public class EliminarTipoMonedaHandler : IRequestHandler<EliminarTipoMonedaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoMonedaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoMonedaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoMoneda tipoMoneda = await this.contexto.TipoMonedas.FindAsync(request.Id);
                if (tipoMoneda == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoMonedas.Remove(tipoMoneda);
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
