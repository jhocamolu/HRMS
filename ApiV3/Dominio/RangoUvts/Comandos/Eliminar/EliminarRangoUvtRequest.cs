using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.RangoUvts.Comandos.Eliminar
{
    public class EliminarRangoUvtRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
