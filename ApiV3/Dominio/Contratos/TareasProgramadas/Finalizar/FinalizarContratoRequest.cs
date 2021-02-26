using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Contratos.TareasProgramadas.Finalizar
{
    public class FinalizarContratoRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }
    }
}
