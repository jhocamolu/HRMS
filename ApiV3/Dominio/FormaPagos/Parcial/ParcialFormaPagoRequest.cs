using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar validaciones para actualziacion parcial
/// de la entidad FormaPago.
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Parcial
{
    public class ParcialFormaPagoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region Nombre
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + "40.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                  ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #region Estado Registro
        public bool? Activo { get; set; }
        #endregion

        #endregion

        #region Validaciones Manueales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var ExisteId = contexto.FormaPagos.FirstOrDefault(x => x.Id == Id);
                if (ExisteId == null)
                {
                    errores.Add(new ValidationResult("No existe Id", new[] { "Id" }));
                    return errores;
                }

                var formaPago = contexto.FormaPagos.FirstOrDefault(x => x.Nombre == Nombre);
                if (formaPago != null)
                {
                    errores.Add(new ValidationResult("La forma de pago que intentas crear ya existe.", new[] { "Nombre" }));
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
