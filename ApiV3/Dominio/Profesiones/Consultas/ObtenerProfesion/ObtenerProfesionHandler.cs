using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Profesiones.Consultas.ObtenerProfesion
{
    public class ObtenerProfesionHandler : IRequestHandler<ObtenerProfesionRequest, Profesion>
    {
        private readonly NominaDbContext context;

        public ObtenerProfesionHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public Task<Profesion> Handle(ObtenerProfesionRequest request, CancellationToken cancellationToken)
        {
            //return this.context.Profesiones;
            throw new NotImplementedException();
        }
    }
}
