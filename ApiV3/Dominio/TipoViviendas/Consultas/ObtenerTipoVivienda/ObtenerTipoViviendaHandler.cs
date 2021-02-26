using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoViviendas.Consultas.ObtenerTipoVivienda
{
    public class ObtenerTipoViviendaHandler : IRequestHandler<ObtenerTipoViviendaRequest, TipoVivienda>
    {
        private readonly NominaDbContext context;

        public ObtenerTipoViviendaHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public async Task<TipoVivienda> Handle(ObtenerTipoViviendaRequest request, CancellationToken cancellationToken)
        {
            return await this.context.TipoViviendas.FindAsync(request.Id);
        }
    }
}
