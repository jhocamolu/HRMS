using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Notificaciones.Comandos.Eliminar
{
    public class EliminarNotificacionRequest : IRequest<CommandResult>
    {
        public int? Id { get; set; }
    }
}
