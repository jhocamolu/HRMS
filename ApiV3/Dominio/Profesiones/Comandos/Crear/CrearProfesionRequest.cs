using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realziar las validaciones para crear
/// registros en la entidad Profesion
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Crear
{
    public class CrearProfesionRequest : IRequest<CommandResult>, IValidatableObject
    {

        #region Validaciones

        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + "60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                 ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var profesion = contexto.Profesiones.FirstOrDefault(x => x.Nombre == Nombre);
                if (profesion != null)
                {
                    errors.Add(new ValidationResult(
                       $"La profesión que intentas guardar ya existe.",
                       new[] { "Nombre" }));
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
