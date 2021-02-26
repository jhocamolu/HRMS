using MediatR;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.ComplementoPlantillas.Comandos.Actualizar
{
    public class ActualizarComplementoPlantillaHandler : IRequestHandler<ActualizarComplementoPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public ActualizarComplementoPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarComplementoPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ComplementoPlantilla complementoPlantilla = contexto.ComplementoPlantillas.FirstOrDefault(z => z.Id == request.Id);
                complementoPlantilla.Nombre = Texto.TipoOracion(request.Nombre);
                complementoPlantilla.Alto = request.Alto;
                complementoPlantilla.Tipo = request.Tipo;
                complementoPlantilla.CuerpoDocumento = request.CuerpoDocumento;
                complementoPlantilla.GrupoDocumentoId = request.GrupoDocumentoId;

                contexto.ComplementoPlantillas.Update(complementoPlantilla);
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
