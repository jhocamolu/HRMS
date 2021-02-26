using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validaciones para actualizaciones parciales en la entidad Operadores de pago
/// </summary>

namespace ApiV3.Dominio.OperadorPagos.Comando.Parcial
{
    public class ParcialOperadorPagoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region Nombre
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #region PaginaWeb
        [Url(ErrorMessage = ConstantesErrores.PaginaWeb)]
        [MaxLength(80, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string PaginaWeb { get; set; }
        #endregion

        #region Estado_Registro
        public bool? Activo { get; set; }
        #endregion

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                var existeId = contexto.OperadorPagos.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult($"No existe un operador de pago con el Id {Id}", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region Nombre
                if (Nombre != null)
                {
                    var nombre = contexto.OperadorPagos.FirstOrDefault(x => x.Id != Id && x.Nombre == Nombre);
                    if (nombre != null)
                    {
                        errores.Add(new ValidationResult("El nombre que intentas guardar ya existe para un operador de pago",
                            new[] { "Nombre" }));
                    }
                }
                #endregion

                #region PaginaWeb
                if (PaginaWeb != null)
                {
                    var paginaWeb = contexto.OperadorPagos.FirstOrDefault(x => x.Id != Id && x.PaginaWeb == PaginaWeb);
                    if (paginaWeb != null)
                    {
                        errores.Add(new ValidationResult("La página web que intentas guardar ya existe para un operador de pago",
                            new[] { "PaginaWeb" }));
                    }
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
