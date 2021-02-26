using ApiV3.Infraestructura.DbContexto;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Profesiones.Consultas.ObtenerProfesiones
{
    public class ObtenerProfesionHandler : IRequestHandler<ObtenerProfesionesRequest, IQueryable>
    {
        private readonly NominaDbContext contexto;
        public ObtenerProfesionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public Task<IQueryable> Handle(ObtenerProfesionesRequest request, CancellationToken cancellationToken)
        {
            //return this.contexto.Profesiones;
            throw new NotImplementedException();
        }
    }
}
