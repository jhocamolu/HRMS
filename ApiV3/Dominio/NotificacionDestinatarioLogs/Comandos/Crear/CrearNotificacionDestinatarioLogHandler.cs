using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NotificacionDestinatarioLogs.Comandos.Crear
{
    public class CrearNotificacionDestinatarioLogHandler : IRequestHandler<CrearNotificacionDestinatarioLogRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearNotificacionDestinatarioLogHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearNotificacionDestinatarioLogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NotificacionDestinatarioLog notificacionDestinatarioLog = new NotificacionDestinatarioLog();
                notificacionDestinatarioLog.NotificacionId = (int)request.NotificacionId;
                if (request.FuncionarioId != null)
                {
                    notificacionDestinatarioLog.FuncionarioId = (int)request.FuncionarioId;
                }
                if (request.CorreoElectronico != null)
                {
                    notificacionDestinatarioLog.CorreoElectronico = request.CorreoElectronico;
                }

                notificacionDestinatarioLog.Estado = (EstadoNotificacion)request.Estado;

                if (request.Resultado != null) notificacionDestinatarioLog.Resultado = request.Resultado;

                this.contexto.NotificacionDestinatarioLogs.Add(notificacionDestinatarioLog);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(notificacionDestinatarioLog);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}