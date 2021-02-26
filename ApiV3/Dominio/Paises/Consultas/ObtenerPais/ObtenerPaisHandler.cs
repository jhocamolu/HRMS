using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Paises.Consultas.Obtener
{
    public class ObtenerPaisHandler : IRequestHandler<ObtenerPaisRequest, Pais>
    {
        private readonly NominaDbContext context;

        public ObtenerPaisHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public async Task<Pais> Handle(ObtenerPaisRequest request, CancellationToken cancellationToken)
        {
            return await this.context.Paises.FindAsync(request.Id);
        }
    }
}
