using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Calse encargada de realizar actualizaciones generales a la
/// entidad profesiones.
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Actualizar
{
    public class ActualizarProfesionRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + "60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                 ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #endregion

        #region Validaciones Manueales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var profesion = contexto.Profesiones.FirstOrDefault(x => x.Id != Id && x.Nombre == Nombre);
                if (profesion != null)
                {
                    errores.Add(new ValidationResult(
                       $"La profesión que intentas actualizar ya existe.",
                       new[] { "Nombre" }));
                }
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
        #endregion
    }
}