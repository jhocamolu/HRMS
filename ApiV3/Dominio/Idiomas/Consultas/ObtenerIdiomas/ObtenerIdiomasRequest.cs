using MediatR;
using System.Linq;

namespace ApiV3.Dominio.Idiomas.Consultas.ObtenerIdiomas
{
    public class ObtenerIdiomasRequest : IRequest<IQueryable>
    {
    }
}
