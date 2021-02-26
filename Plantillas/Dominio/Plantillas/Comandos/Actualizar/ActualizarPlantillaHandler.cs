using MediatR;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.Plantillas.Comandos.Actualizar
{
    public class ActualizarPlantillaHandler : IRequestHandler<ActualizarPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public ActualizarPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Plantilla plantillaDocumento = contexto.Plantillas.FirstOrDefault(z => z.Id == request.Id);
                plantillaDocumento.Nombre = Texto.TipoOracion(request.Nombre);
                plantillaDocumento.EncabezadoId = request.EncabezadoId;
                plantillaDocumento.PiePaginaId = request.PiePaginaId;
                plantillaDocumento.CuerpoDocumento = request.CuerpoDocumento;
                plantillaDocumento.GrupoDocumentoId = request.GrupoDocumentoId;
                plantillaDocumento.DocumentoId = request.DocumentoId;
                plantillaDocumento.Version = request.Version;
                plantillaDocumento.FechaVigencia = (DateTime)request.FechaVigencia;

                contexto.Plantillas.Update(plantillaDocumento);
                await contexto.SaveChangesAsync();
                plantillaDocumento.GrupoDocumento = null;
                plantillaDocumento.Documento = null;
                plantillaDocumento.ComplementoEncabezado = null;
                plantillaDocumento.ComplementoPiePagina = null;

                return CommandResult.Success(plantillaDocumento);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
