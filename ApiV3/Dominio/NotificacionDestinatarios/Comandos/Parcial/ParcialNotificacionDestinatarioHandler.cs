using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NotificacionDestinatarios.Comandos.Parcial
{
    public class ParcialNotificacionDestinatarioHandler : IRequestHandler<ParcialNotificacionDestinatarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialNotificacionDestinatarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialNotificacionDestinatarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NotificacionDestinatario notificacionDestinatario = await contexto.NotificacionDestinatarios.FindAsync(request.Id);

                if (request.NotificacionId != null) notificacionDestinatario.NotificacionId = (int)request.NotificacionId;
                if (request.FuncionarioId != null) notificacionDestinatario.FuncionarioId = (int)request.FuncionarioId;
                if (request.Estado != null) notificacionDestinatario.Estado = (EstadoNotificacion)request.Estado;
                if (request.CorreoElectronico != null) notificacionDestinatario.CorreoElectronico = request.CorreoElectronico;

                if (request.Activo != null)
                {
                    notificacionDestinatario.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false) notificacionDestinatario.EstadoRegistro = EstadoRegistro.Eliminado;
                }

                this.contexto.NotificacionDestinatarios.Update(notificacionDestinatario);
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
