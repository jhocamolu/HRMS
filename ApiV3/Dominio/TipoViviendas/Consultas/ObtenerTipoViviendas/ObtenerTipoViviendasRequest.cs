using MediatR;
using System.Linq;

namespace ApiV3.Dominio.TipoViviendas.Consultas.ObtenerTipoViviendas
{
    public class ObtenerTipoViviendasRequest : IRequest<IQueryable>
    {
    }
}
