using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Eliminar
{
    public class EliminarSolicitudCesantiaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
