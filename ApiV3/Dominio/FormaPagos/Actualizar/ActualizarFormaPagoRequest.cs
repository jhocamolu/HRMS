using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar la Actualizacion de registros en la entidad formaPago
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Actualizar
{
    public class ActualizarFormaPagoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + "40.")]
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

                #region Id
                var id = contexto.FormaPagos.FirstOrDefault(x => x.Id == Id);
                if (id == null)
                {
                    errores.Add(new ValidationResult("No existe Id", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region Nombre
                var formaPago = contexto.FormaPagos.FirstOrDefault(x => x.Id != Id && x.Nombre == Nombre);
                if (formaPago != null)
                {
                    errores.Add(new ValidationResult(
                        $"La forma de pago que intentas actualizar ya existe.",
                        new[] { "Nombre" }));
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
