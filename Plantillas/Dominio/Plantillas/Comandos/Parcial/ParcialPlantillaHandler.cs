using MediatR;
using Plantillas.Dominio.Enumerador;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.Plantillas.Comandos.Parcial
{
    public class ParcialPlantillaHandler : IRequestHandler<ParcialPlantillaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public ParcialPlantillaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialPlantillaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Plantilla plantillaDocumento = contexto.Plantillas.FirstOrDefault(z => z.Id == request.Id);

                if (request.Activo != null)
                {
                    if ((bool)request.Activo)
                    {
                        plantillaDocumento.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        plantillaDocumento.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                if (null != request.GrupoDocumentoId)
                {
                    plantillaDocumento.GrupoDocumentoId = (int)request.GrupoDocumentoId;
                }

                if (null != request.DocumentoId)
                {
                    plantillaDocumento.DocumentoId = (int)request.DocumentoId;
                }
                if (request.Nombre != null)
                {
                    plantillaDocumento.Nombre = Texto.TipoOracion(request.Nombre);
                }

                if (request.EncabezadoId != null)
                {
                    plantillaDocumento.EncabezadoId = request.EncabezadoId;
                }

                if (request.PiePaginaId != null)
                {
                    plantillaDocumento.PiePaginaId = request.PiePaginaId;
                }

                if (request.CuerpoDocumento != null)
                {
                    plantillaDocumento.CuerpoDocumento = request.CuerpoDocumento;
                }

                if (request.Version != null)
                {
                    plantillaDocumento.Version = request.Version;
                }

                if (request.FechaVigencia != null)
                {
                    plantillaDocumento.FechaVigencia = (DateTime)request.FechaVigencia;
                }

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
