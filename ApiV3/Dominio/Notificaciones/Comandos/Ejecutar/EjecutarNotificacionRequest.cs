using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Notificaciones.Comandos.Ejecutar
{
    public class EjecutarNotificacionRequest : IRequest<CommandResult>
    {
        public int? Id { get; set; }
    }
}
