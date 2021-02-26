using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CentroTrabajos.Consultas.ObtenerCentroTrabajo
{
    public class ObtenerCentroTrabajoHandler : IRequestHandler<ObtenerCentroTrabajoRequest, CentroTrabajo>
    {
        private readonly NominaDbContext context;

        public ObtenerCentroTrabajoHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public async Task<CentroTrabajo> Handle(ObtenerCentroTrabajoRequest request, CancellationToken cancellationToken)
        {
            return await this.context.CentroTrabajos.FindAsync(request.Id);
        }
    }
}
