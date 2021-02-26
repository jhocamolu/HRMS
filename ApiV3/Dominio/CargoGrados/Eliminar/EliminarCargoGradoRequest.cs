using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.CargoGrados.Eliminar
{
    public class EliminarCargoGradoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
