using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Idiomas.Comandos.Eliminar
{
    public class EliminarIdiomaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones 
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Id { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var elemento = dbContexto.Idiomas.SingleOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
            }
            catch (Exception)
            {

            }
            return errores;
        }
        #endregion
    }
}
