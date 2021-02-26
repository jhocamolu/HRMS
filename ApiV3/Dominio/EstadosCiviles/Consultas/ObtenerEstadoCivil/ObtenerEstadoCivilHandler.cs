using ApiV3.Infraestructura.DbContexto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EstadosCiviles.Consultas.ObtenerEstadoCivil
{
    public class ObtenerEstadoCivilHandler : IRequestHandler<ObtenerEstadoCivilRequest, IQueryable>
    {
        private readonly NominaDbContext _context;

        public ObtenerEstadoCivilHandler(NominaDbContext context)
        {
            _context = context;
        }


        public Task<IQueryable> Handle(ObtenerEstadoCivilRequest request, CancellationToken cancellationToken)
        {

            // IQueryable<EstadoCivil> Consulta = (from EstadoCivil in _context.EstadoCiviles
            //                                     where EstadoCivil.Id == request.Id
            //                                     select EstadoCivil);
            // return Consulta;
            throw new NotImplementedException();
        }
    }
}
