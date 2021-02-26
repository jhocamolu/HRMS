using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoBeneficioRequisitos.Comandos.Crear
{
    public class CrearTipoBeneficioRequisitoRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int TipoBeneficioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int TipoSoporteId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                //Valida que código sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region TipoBeneficioId
                var tipoBeneficio = dbContexto.TipoBeneficios.FirstOrDefault(x => x.Id == TipoBeneficioId);
                if (tipoBeneficio == null)
                {
                    errores.Add(new ValidationResult(
                        $"El tipo de beneficio que intentas guardar no existe.",
                        new[] { "TipoBeneficioId" }));
                }
                #endregion
                #region TipoSoporteId
                var tipoSoporte = dbContexto.TipoSoportes.FirstOrDefault(x => x.Id == TipoSoporteId);
                if (tipoSoporte == null)
                {
                    errores.Add(new ValidationResult(
                        $"El tipo de soporte que intentas guardar no existe.",
                        new[] { "TipoSoporteId" }));
                }

                var validatipoSoporte = dbContexto.TipoBeneficioRequisitos.FirstOrDefault(x => x.TipoSoporteId == TipoSoporteId && x.TipoBeneficioId == TipoBeneficioId);
                if (validatipoSoporte != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre que intentas guardar ya existe.",
                        new[] { "TipoSoporteId" }));
                }
                #endregion

            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }

            return errores;
        }
    }
}
