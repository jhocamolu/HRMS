using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoHoraExtras.Comandos.Eliminar
{
    public class EliminarTipoHoraExtraHandler : IRequestHandler<EliminarTipoHoraExtraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoHoraExtra tipoHoraExtra = await this.contexto.TipoHoraExtras.FindAsync(request.Id);
                if (tipoHoraExtra == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoHoraExtras.Remove(tipoHoraExtra);
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
