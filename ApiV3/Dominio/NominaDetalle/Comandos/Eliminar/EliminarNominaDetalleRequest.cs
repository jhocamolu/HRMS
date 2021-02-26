using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.NominaDetalle.Comandos.Eliminar
{
    public class EliminarNominaDetalleRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
