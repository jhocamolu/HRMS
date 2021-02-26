using ApiV3.Infraestructura.DbContexto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CentroTrabajos.Consultas.ObtenerCentroTrabajos
{
    public class ObtenerCentroTrabajosHandler : IRequestHandler<ObtenerCentroTrabajosRequest, IQueryable>
    {
        private readonly NominaDbContext context;

        public ObtenerCentroTrabajosHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public Task<IQueryable> Handle(ObtenerCentroTrabajosRequest request, CancellationToken cancellationToken)
        {
            //return this.context.CentroTrabajos;
            throw new NotImplementedException();
        }
    }
}
