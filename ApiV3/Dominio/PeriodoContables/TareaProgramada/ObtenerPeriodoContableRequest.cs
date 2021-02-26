using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.PeriodoContables.TareaProgramada
{
    public class ObtenerPeriodoContableRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }
    }

}
