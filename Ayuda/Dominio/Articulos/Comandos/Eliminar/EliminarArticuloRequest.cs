using Ayuda.Dominio.Utilidades;
using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Comandos.Eliminar
{
    public class EliminarArticuloRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<string> Palabras { get;}
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                var dbContext = (AyudaDbContext)validationContext.GetService(typeof(AyudaDbContext));
                var element = dbContext.Articulos.FirstOrDefault(x => x.Id == Id);
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
