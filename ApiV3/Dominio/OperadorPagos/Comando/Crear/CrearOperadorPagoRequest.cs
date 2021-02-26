using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validaciones para crear registros en la entidad Operadores de pago
/// </summary>

namespace ApiV3.Dominio.OperadorPagos.Comando.Crear
{
    public class CrearOperadorPagoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #region PaginaWeb
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255")]
        [Url(ErrorMessage = ConstantesErrores.PaginaWeb)]
        public string PaginaWeb { get; set; }
        #endregion

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Nombre
                var nombre = contexto.OperadorPagos.FirstOrDefault(x => x.Nombre == Nombre);
                if (nombre != null)
                {
                    errores.Add(new ValidationResult("El nombre que intentas guardar ya existe para un operador de pago",
                        new[] { "Nombre" }));
                }
                #endregion

                #region PaginaWeb
                var paginaWeb = contexto.OperadorPagos.FirstOrDefault(x => x.PaginaWeb == PaginaWeb);
                if (paginaWeb != null)
                {
                    errores.Add(new ValidationResult("La página web que intentas guardar ya existe para un operador de pago",
                        new[] { "PaginaWeb" }));
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
