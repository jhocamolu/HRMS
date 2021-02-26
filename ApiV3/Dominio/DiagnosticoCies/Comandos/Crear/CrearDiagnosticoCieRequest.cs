using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validaciones de formato para la entidad DiagnosticoCie
/// No se puede repetir el codigo,se valida en la region #region Validaciones Manuales
/// </summary>

namespace ApiV3.Dominio.DiagnosticoCies.Comandos.Crear
{
    public class CrearDiagnosticoCieRequest : IRequest<CommandResult>, IValidatableObject
    {

        #region Validaciones

        #region Codigo
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(4, ErrorMessage = ConstantesErrores.Maximo + "4.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }
        #endregion

        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Nombre { get; set; }
        #endregion
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var diagnosticoCie = contexto.DiagnosticoCies.FirstOrDefault(x => x.Codigo == Codigo);
                if (diagnosticoCie != null)
                {
                    errores.Add(new ValidationResult("El código de diagnóstico que intentas guardar ya existe.",
                        new[] { "Codigo" }));
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
