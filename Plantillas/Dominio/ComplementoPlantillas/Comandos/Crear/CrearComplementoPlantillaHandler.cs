using MediatR;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.ComplementoPlantillas.Comandos.Crear
{
    public class CrearComplementoPlantillaHandler : IRequestHandler<CrearComplementoPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public CrearComplementoPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearComplementoPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ComplementoPlantilla complementoPlantilla = new ComplementoPlantilla
                {
                    Nombre = Texto.TipoOracion(request.Nombre),
                    Alto = request.Alto,
                    Tipo = request.Tipo,
                    CuerpoDocumento = request.CuerpoDocumento,
                    GrupoDocumentoId = request.GrupoDocumentoId
                };

                contexto.ComplementoPlantillas.Add(complementoPlantilla);

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
