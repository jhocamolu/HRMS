using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NotificacionDestinatarios.Comandos.Eliminar
{
    public class EliminarNotificacionDestinatarioHandler : IRequestHandler<EliminarNotificacionDestinatarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarNotificacionDestinatarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNotificacionDestinatarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NotificacionDestinatario notificacionDestinatario = await this.contexto.NotificacionDestinatarios.FindAsync(request.Id);
                if (notificacionDestinatario == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                contexto.NotificacionDestinatarios.Remove(notificacionDestinatario);
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
