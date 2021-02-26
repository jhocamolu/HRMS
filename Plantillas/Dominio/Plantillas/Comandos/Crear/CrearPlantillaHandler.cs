using MediatR;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.Plantillas.Comandos.Crear
{
    public class CrearPlantillaHandler : IRequestHandler<CrearPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public CrearPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime fecha = (DateTime)request.FechaVigencia;
                if (request.FechaVigencia == null)
                {
                    fecha = DateTime.Now;
                }

                Plantilla plantillaDocumento = new Plantilla
                {
                    Nombre = Texto.TipoOracion(request.Nombre),
                    EncabezadoId = request.EncabezadoId,
                    PiePaginaId = request.PiePaginaId,
                    CuerpoDocumento = request.CuerpoDocumento,
                    GrupoDocumentoId = request.GrupoDocumentoId,
                    Version = request.Version,
                    FechaVigencia = fecha,
                    DocumentoId = request.DocumentoId
                };

                contexto.Plantillas.Add(plantillaDocumento);

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
