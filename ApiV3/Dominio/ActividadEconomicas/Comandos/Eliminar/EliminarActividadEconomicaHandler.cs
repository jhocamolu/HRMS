using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadEconomicas.Comandos.Eliminar
{
    public class EliminarActividadEconomicaHandler : IRequestHandler<EliminarActividadEcomonicaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarActividadEconomicaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarActividadEcomonicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadEconomica actividaEconomica = await this.contexto.ActividadEconomicas.FindAsync(request.Id);
                if (actividaEconomica == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.ActividadEconomicas.Remove(actividaEconomica);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
