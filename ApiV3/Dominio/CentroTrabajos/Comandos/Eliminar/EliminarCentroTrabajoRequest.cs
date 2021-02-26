using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.CentroTrabajos.Comandos.Eliminar
{
    public class EliminarCentroTrabajoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
