using MediatR;
using System.Linq;

namespace ApiV3.Dominio.Paises.Consultas.ObtenerPaises
{
    public class ObtenerPaisesRequest : IRequest<IQueryable>
    {

    }
}
