using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoViviendas.Consultas.ObtenerTipoViviendas
{
    public class ObtenerTipoViviendasHandler : IRequestHandler<ObtenerTipoViviendasRequest, IQueryable>
    {
        public Task<IQueryable> Handle(ObtenerTipoViviendasRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //return this.context.TipoViviendas;
        }
    }
}
