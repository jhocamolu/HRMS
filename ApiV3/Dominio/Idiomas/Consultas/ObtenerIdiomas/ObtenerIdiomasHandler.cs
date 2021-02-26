using ApiV3.Infraestructura.DbContexto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Idiomas.Consultas.ObtenerIdiomas
{
    public class ObtenerIdiomasHandler : IRequestHandler<ObtenerIdiomasRequest, IQueryable>
    {
        private readonly NominaDbContext context;

        public ObtenerIdiomasHandler(NominaDbContext context)
        {
            this.context = context;
        }
        public Task<IQueryable> Handle(ObtenerIdiomasRequest request, CancellationToken cancellationToken)
        {
            // return context.Idiomas;
            throw new NotImplementedException();
        }
    }
}
