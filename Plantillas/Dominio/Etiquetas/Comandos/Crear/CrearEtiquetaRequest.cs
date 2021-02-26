using MediatR;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plantillas.Dominio.Etiquetas.Comandos.Crear
{
    public class CrearEtiquetaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string GrupoDocumentoSlug { get; set; }

        [Required]
        public string Cargo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (PlantillasDbContext)validationContext.GetService(typeof(PlantillasDbContext));

                var nombre = dbContexto.Etiquetas.FirstOrDefault(x => x.Nombre == Nombre);
                if (nombre != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre de la etiqueta que intentas guardar ya existe.",
                        new[] { "Nombre" }));
                }

                var slug = dbContexto.Etiquetas.FirstOrDefault(x => x.Slug == Slug);
                if (slug != null)
                {
                    errores.Add(new ValidationResult(
                        $"El slug de la etiqueta que intentas guardar ya existe.",
                        new[] { "Slug" }));
                }

                var grupoDocumentoSlug = dbContexto.GrupoDocumentos.FirstOrDefault(x => x.Slug == Slug);
                if (grupoDocumentoSlug == null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre de la etiqueta que intentas guardar ya existe.",
                        new[] { "grupoDocumentoSlug" }));
                }
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
    }
}
