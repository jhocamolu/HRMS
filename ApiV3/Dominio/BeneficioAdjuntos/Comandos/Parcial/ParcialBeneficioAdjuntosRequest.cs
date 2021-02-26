using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.BeneficioAdjuntos.Comandos.Parcial
{
    public class ParcialBeneficioAdjuntosRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? BeneficioId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoBeneficioRequisitoId { get; set; }


        public string Adjunto { get; set; }

        #region Activo
        public bool? Activo { get; set; }
        #endregion
        #endregion

        #region Validaciones
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region ExisteId
                var existeId = contexto.BeneficioAdjuntos.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("beneficio Adjunto"), new[] { "Id" }));
                    return errores;
                }
                #endregion
                if (BeneficioId != null)
                {
                    var beneficioId = contexto.Beneficios.FirstOrDefault(x => x.Id == BeneficioId);
                    if (beneficioId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("Beneficio"), new[] { "BeneficioId" }));
                        return errores;
                    }
                }
                #region TipoBeneficioRequisitoId
                if (TipoBeneficioRequisitoId != null)
                {
                    var beneficioId = contexto.TipoBeneficioRequisitos.FirstOrDefault(x => x.Id == TipoBeneficioRequisitoId);
                    if (beneficioId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo beneficio requisito id"), new[] { "TipoBeneficioRequisitoId" }));
                        return errores;
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