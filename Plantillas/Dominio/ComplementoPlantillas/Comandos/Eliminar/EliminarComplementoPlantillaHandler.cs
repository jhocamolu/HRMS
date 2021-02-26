using MediatR;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.ComplementoPlantillas.Comandos.Eliminar
{
    public class EliminarComplementoPlantillaHandler : IRequestHandler<EliminarComplementoPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public EliminarComplementoPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarComplementoPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ComplementoPlantilla complementoPlantilla = contexto.ComplementoPlantillas.FirstOrDefault(z => z.Id == request.Id);
                contexto.ComplementoPlantillas.Remove(complementoPlantilla);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(complementoPlantilla);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
