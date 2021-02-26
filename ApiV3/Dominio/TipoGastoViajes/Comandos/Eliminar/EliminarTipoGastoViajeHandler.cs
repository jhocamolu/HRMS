using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoGastoViajes.Comandos.Eliminar
{
    public class EliminarTipoGastoViajeHandler : IRequestHandler<EliminarTipoGastoViajeRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoGastoViajeHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoGastoViajeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoGastoViaje tipoGastoViaje = contexto.TipoGastoViajes.Find(request.Id);
                if (tipoGastoViaje == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoGastoViajes.Remove(tipoGastoViaje);
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
