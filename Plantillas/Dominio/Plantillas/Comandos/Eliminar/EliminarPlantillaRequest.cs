using MediatR;
using Plantillas.Dominio.Utilidades;
using Plantillas.Infraestructura.Resultados;
using Plantillas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plantillas.Dominio.Plantillas.Comandos.Eliminar
{
    public class EliminarPlantillaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (PlantillasDbContext)validationContext.GetService(typeof(PlantillasDbContext));
                var existe = dbContexto.Plantillas.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                    return errores;
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
