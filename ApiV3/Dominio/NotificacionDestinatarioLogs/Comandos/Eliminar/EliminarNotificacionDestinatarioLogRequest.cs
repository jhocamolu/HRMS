using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.NotificacionDestinatarioLogs.Comandos.Eliminar
{
    public class EliminarNotificacionDestinatarioLogRequest : IRequest<CommandResult>
    {
        public int? Id { get; set; }
    }
}
