using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validaciones para crear registros en la entidad
/// formapago
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Crear
{
    public class CrearFormaPagoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + "40.")]
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
                var formaPago = contexto.FormaPagos.FirstOrDefault(x => x.Nombre == Nombre);
                if (formaPago != null)
                {
                    errors.Add(new ValidationResult("La forma de pago que intentas crear ya existe.", new[] { "Nombre" }));
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
