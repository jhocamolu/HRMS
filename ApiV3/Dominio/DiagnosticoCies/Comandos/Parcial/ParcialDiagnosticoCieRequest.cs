using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validaciones de formato para la actualizacion parcial 
/// de la entidad DiagnosticoCie
/// </summary>

namespace ApiV3.Dominio.DiagnosticoCies.Comandos.Parcial
{
    public class ParcialDiagnosticoCieRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion


        #region Codigo
        [MaxLength(4, ErrorMessage = ConstantesErrores.Maximo + "4.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }
        #endregion

        #region Nombre
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Nombre { get; set; }
        #endregion

        #region Activo
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

                var ExisteId = contexto.DiagnosticoCies.FirstOrDefault(x => x.Id == Id);
                if (ExisteId == null)
                {
                    errores.Add(new ValidationResult("No existe Id.", new[] { "Id" }));
                    return errores;
                }

                var diagnosticoCie = contexto.DiagnosticoCies.FirstOrDefault(x => x.Id == Id);
                if (diagnosticoCie == null)
                {
                    errores.Add(new ValidationResult("El código de diagnóstico que intentas guardar ya existe.",
                        new[] { "Id" }));
                }


                var diagnosticoCieCodigo = contexto.DiagnosticoCies.FirstOrDefault(x => x.Id != Id && x.Codigo == Codigo);
                if (diagnosticoCieCodigo != null)
                {
                    errores.Add(new ValidationResult("Ya existe un diagnóstico con el código ingresado.",
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
