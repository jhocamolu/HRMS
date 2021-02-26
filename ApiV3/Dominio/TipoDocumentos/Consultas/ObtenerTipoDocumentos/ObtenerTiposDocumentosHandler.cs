using ApiV3.Infraestructura.DbContexto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoDocumentos.Consultas.ObtenerTiposDocumentos
{
    public class ObtenerTiposDocumentosHandler : IRequestHandler<ObtenerTiposDocumentosRequest, IQueryable>
    {
        private readonly NominaDbContext Context;
        public ObtenerTiposDocumentosHandler(NominaDbContext context)
        {
            Context = context;
        }

        public Task<IQueryable> Handle(ObtenerTiposDocumentosRequest request, CancellationToken cancellationToken)
        {
            //return Context.TipoDocumentos;
            throw new NotImplementedException();
        }
    }
}