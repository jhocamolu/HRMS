using MediatR;
using System.Linq;

namespace ApiV3.Dominio.Profesiones.Consultas.ObtenerProfesiones
{
    public class ObtenerProfesionesRequest : IRequest<IQueryable>
    {
    }
}
