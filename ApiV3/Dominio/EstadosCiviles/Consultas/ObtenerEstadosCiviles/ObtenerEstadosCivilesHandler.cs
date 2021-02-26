using ApiV3.Infraestructura.DbContexto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EstadosCiviles.Consultas.ObtenerEstadosCiviles
{
    public class ObtenerEstadosCivilesHandler : IRequestHandler<ObtenerEstadosCivilesRequest, IQueryable>
    {

        private readonly NominaDbContext _context;

        public ObtenerEstadosCivilesHandler(NominaDbContext context)
        {
            _context = context;
        }

        public Task<IQueryable> Handle(ObtenerEstadosCivilesRequest request, CancellationToken cancellationToken)
        {
            //return _context.EstadoCiviles;
            throw new NotImplementedException();
        }
    }
}
