using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoDocumentos.Consultas.ObtenerTipoDocuemnto
{
    public class ObtenerTipoDocumentoHandler : IRequestHandler<ObtenerTipoDocumentoRequest, TipoDocumento>
    {

        private readonly NominaDbContext contexto;
        public ObtenerTipoDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<TipoDocumento> Handle(ObtenerTipoDocumentoRequest request, CancellationToken cancellationToken)
        {
            var TipoDocumento = await contexto.TipoDocumentos.FindAsync(request.Id);
            return TipoDocumento;
        }
    }
}
