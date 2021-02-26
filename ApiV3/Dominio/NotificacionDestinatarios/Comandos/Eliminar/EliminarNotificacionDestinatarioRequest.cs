using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.NotificacionDestinatarios.Comandos.Eliminar
{
    public class EliminarNotificacionDestinatarioRequest : IRequest<CommandResult>
    {
        public int? Id { get; set; }
    }
}
