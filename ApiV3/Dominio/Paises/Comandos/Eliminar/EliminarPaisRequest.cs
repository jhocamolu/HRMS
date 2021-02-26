using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de Eliminar el tipoDocumento.
/// </summary>

namespace ApiV3.Dominio.Paises.Comandos.Eliminar
{
    public class EliminarPaisRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                //Valida si existe el id a eliminar
                var dbContext = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var element = dbContext.Paises.SingleOrDefault(x => x.Id == Id);
                if (element == null)
                {
                    errors.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
            }
            catch (Exception e)
            {
                errors.Add(new ValidationResult(e.Message));
            }
            return errors;
        }
        #endregion
    }
}