using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SubPeriodos.Comandos.Crear
{
    public class CrearSubPeriodoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico
            + ConstantesExpresionesRegulares.Alfabetico + " ]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 1000, ErrorMessage = ConstantesErrores.Rango + "1 - 1000.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? Dias { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoPeriodoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 31, ErrorMessage = ConstantesErrores.Rango + "1 - 40.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? DiaInicial { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Nombre
                var validaUnico = contexto.SubPeriodos.FirstOrDefault(x => x.Nombre == Nombre);
                if (validaUnico != null)
                {
                    errores.Add(new ValidationResult(
                       $"El subperíodo que intentas guardar ya existe.",
                       new[] { "Nombre" }));
                }
                //Valida que tipo de Período exista
                if (TipoPeriodoId != null)
                {
                    var validaTipoPeriodo = contexto.TipoPeriodos.FirstOrDefault(x => x.Id == TipoPeriodoId);
                    if (validaTipoPeriodo == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo periodo"),
                            new[] { "TipoPeriodoId" }));
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
