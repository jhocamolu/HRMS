using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Contratos.Consultas
{
    public class ObtenerContratoDatosActualesRequest : IRequest<CommandResult>
    {
        public int ContratoId { get; set; }
    }
}