using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NomenclaturaDians.Comandos.Parcial
{
    public class ParcialNomenclaturaDianRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                var dbContext = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id

                // Elemento no existe
                var existe = dbContext.NomenclaturaDians.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errors.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));

                    return errors;
                }
                #endregion
            }
            catch (Exception e)
            {
                errors.Add(new ValidationResult(e.Message));
            }

            return errors;
        }
    }
}
