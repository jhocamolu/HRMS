using MediatR;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.Plantillas.Comandos.Eliminar
{
    public class EliminarPlantillaHandler : IRequestHandler<EliminarPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public EliminarPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Plantilla plantillaDocumento = contexto.Plantillas.FirstOrDefault(z => z.Id == request.Id);
                contexto.Plantillas.Remove(plantillaDocumento);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(plantillaDocumento);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
