using MediatR;
using Plantillas.Infraestructura;
using Plantillas.Infraestructura.Resultados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plantillas.Dominio.CrearPdf.Comando.CrearPdf
{
    public class CrearPdfRequest : EnumerableRequest, IRequest<CommandResult>, IValidatableObject
    {
        public string DocumentoSlug { get; set; }

        public int Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
