using MediatR;
using System.Linq;

namespace ApiV3.Dominio.TipoDocumentos.Consultas.ObtenerTiposDocumentos
{
    public class ObtenerTiposDocumentosRequest : IRequest<IQueryable>
    {
    }
}
