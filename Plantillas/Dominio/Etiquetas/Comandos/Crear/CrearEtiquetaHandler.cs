using MediatR;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plantillas.Dominio.Etiquetas.Comandos.Crear
{
    public class CrearEtiquetaHandler : IRequestHandler<CrearEtiquetaRequest, CommandResult>
    {
        private readonly PlantillasDbContext contexto;

        public CrearEtiquetaHandler(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearEtiquetaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Etiqueta etiqueta = new Etiqueta
                {
                    Nombre = request.Nombre,
                    Slug = request.Slug
                };
                contexto.Etiquetas.Add(etiqueta);
                await contexto.SaveChangesAsync();

                GrupoDocumento grupoId = contexto.GrupoDocumentos.FirstOrDefault(x => x.Slug == request.GrupoDocumentoSlug);
                ServicioFijo servicio = contexto.ServicioFijos.FirstOrDefault(x => x.Nombre == "RepresentanteEmpresa");

                GrupoDocumentoEtiqueta documentoEtiqueta = new GrupoDocumentoEtiqueta
                {
                    GrupoDocumentoId = grupoId.Id,
                    EtiquetaId = etiqueta.Id,
                    ServicioFijoId = servicio.Id
                };

                contexto.GrupoDocumentoEtiquetas.Add(documentoEtiqueta);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(etiqueta);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.InnerException.Message);
            }
        }
    }
}
