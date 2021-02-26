using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Eliminar
{
    public class EliminarCategoriaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                var dbContext = (AyudaDbContext)validationContext.GetService(typeof(AyudaDbContext));
                var element = dbContext.Categorias.FirstOrDefault(x => x.Id == Id);
                if (element == null)
                {
                    errors.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
            }
            catch (Exception)
            {

            }
            return errors;
        }
    }
}
