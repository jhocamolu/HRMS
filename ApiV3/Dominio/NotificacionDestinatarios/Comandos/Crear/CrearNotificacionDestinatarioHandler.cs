using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NotificacionDestinatarios.Comandos.Crear
{
    public class CrearNotificacionDestinatarioHandler : IRequestHandler<CrearNotificacionDestinatarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearNotificacionDestinatarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearNotificacionDestinatarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NotificacionDestinatario notificacionDestinatario = new NotificacionDestinatario();
                notificacionDestinatario.NotificacionId = (int)request.NotificacionId;
                if (request.FuncionarioId != null)
                {
                    notificacionDestinatario.FuncionarioId = request.FuncionarioId;
                }
                if (request.CorreoElectronico != null)
                {
                    notificacionDestinatario.CorreoElectronico = request.CorreoElectronico;
                }
                notificacionDestinatario.Estado = EstadoNotificacion.Pendiente;

                this.contexto.NotificacionDestinatarios.Add(notificacionDestinatario);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(notificacionDestinatario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
