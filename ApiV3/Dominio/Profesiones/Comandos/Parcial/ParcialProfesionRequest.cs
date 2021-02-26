using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validacion de  formato para las
/// actualizaciones parciales de la entidad Profesion.
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Parcial
{
    public class ParcialProfesionRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region Nombre
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + "60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                 ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #region Estado Registro
        public bool? Activo { get; set; }
        #endregion

        #endregion

        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                var profesion = contexto.Profesiones.FirstOrDefault(x => x.Id == Id);
                if (profesion == null)
                {
                    errores.Add(new ValidationResult(
                        "La profesión que intentas actualizar no  existe.",
                        new[] { "EstadoRegistro" }));
                }
                #endregion
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