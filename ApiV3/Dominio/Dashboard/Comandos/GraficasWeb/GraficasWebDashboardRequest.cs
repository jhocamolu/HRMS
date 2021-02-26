using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Dashboard.Comandos.GraficasWeb
{
    public class GraficasWebDashboardRequest : IRequest<CommandResult>
    {
        public string aplicacion { get; set; }
    }
}
