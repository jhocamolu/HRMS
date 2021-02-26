using MediatR;
using System.Linq;

namespace ApiV3.Dominio.CentroTrabajos.Consultas.ObtenerCentroTrabajos
{
    public class ObtenerCentroTrabajosRequest : IRequest<IQueryable>
    {
    }
}
