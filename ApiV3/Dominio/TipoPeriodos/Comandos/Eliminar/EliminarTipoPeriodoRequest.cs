using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoPeriodos.Comandos.Eliminar
{
    public class EliminarTipoPeriodoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}