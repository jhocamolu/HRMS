using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NotificacionDestinatarioLogs.Comandos.Eliminar
{
    public class EliminarNotificacionDestinatarioLogHandler : IRequestHandler<EliminarNotificacionDestinatarioLogRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarNotificacionDestinatarioLogHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNotificacionDestinatarioLogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NotificacionDestinatarioLog notificacionDestinatarioLog = await this.contexto.NotificacionDestinatarioLogs.FindAsync(request.Id);
                if (notificacionDestinatarioLog == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                contexto.NotificacionDestinatarioLogs.Remove(notificacionDestinatarioLog);
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
