using ApiV3.Infraestructura.DbContexto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Paises.Consultas.ObtenerPaises
{
    public class ObtenerPaisesHandler : IRequestHandler<ObtenerPaisesRequest, IQueryable>
    {
        private readonly NominaDbContext context;

        public ObtenerPaisesHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public Task<IQueryable> Handle(ObtenerPaisesRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //return this.context.Paises;
        }
    }
}
