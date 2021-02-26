using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.AusentismoFuncionarios.TareaProgramada.Finalizar
{
    public class FinalizarAusentismoRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }
    }
}
