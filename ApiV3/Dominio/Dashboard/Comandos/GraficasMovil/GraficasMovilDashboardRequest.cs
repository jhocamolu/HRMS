using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Dashboard.Comandos.GraficasMovil
{
    public class GraficasMovilDashboardRequest : IRequest<CommandResult>
    {
        public int FuncionarioId { get; set; }
    }
}
