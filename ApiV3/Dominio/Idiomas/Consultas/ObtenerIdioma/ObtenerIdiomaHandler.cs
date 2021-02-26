using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Idiomas.Consultas.ObtenerIdioma
{
    public class ObtenerIdiomaHandler : IRequestHandler<ObtenerIdiomaRequest, Idioma>
    {
        private readonly NominaDbContext context;

        public ObtenerIdiomaHandler(NominaDbContext context)
        {
            this.context = context;
        }
        public async Task<Idioma> Handle(ObtenerIdiomaRequest request, CancellationToken cancellationToken)
        {
            return await context.Idiomas.FindAsync(request.Id);
        }
    }
}
