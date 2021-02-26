using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Cargos.Eliminar
{
    public class EliminarCargoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
