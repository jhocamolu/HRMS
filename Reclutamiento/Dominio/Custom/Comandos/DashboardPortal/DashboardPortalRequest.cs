
using MediatR;
using Reclutamiento.Infraestructura.Resultados;

namespace Reclutamiento.Dominio.Dashboard.Comandos.DashboarPortal
{
    public class DashboardPortalRequest : IRequest<CommandResult>
    {
        public string NumeroDocumento { get; set; }
    }
}
