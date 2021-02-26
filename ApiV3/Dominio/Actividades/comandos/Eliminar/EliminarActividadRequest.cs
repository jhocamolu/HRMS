using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Actividades.comandos.Eliminar
{
    public class EliminarActividadRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
