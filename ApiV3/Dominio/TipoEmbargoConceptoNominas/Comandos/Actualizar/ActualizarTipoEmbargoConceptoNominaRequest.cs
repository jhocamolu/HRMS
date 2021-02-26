using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Actualizar
{
    public class ActualizarTipoEmbargoConceptoNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoEmbargoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ConceptoNominaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1.00, 100.00, ErrorMessage = ConstantesErrores.Rango + "1 - 100.")]
        public double? MaximoEmbargarConcepto { get; set; }

        #endregion
        #region ValidacionesManuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            try
            {
                //Valida que registro sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                // Valida Concepto Nomina
                var validaConceptoNomina = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId);
                if (validaConceptoNomina == null)
                {
                    errors.Add(new ValidationResult(
                        $"No existe.",
                        new[] { "ConceptoNominaId" }));
                }
                else
                {
                    var validarTipoEmbargo = dbContexto.TipoEmbargoConceptoNominas
                                                        .FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId
                                                                        && x.TipoEmbargoId == TipoEmbargoId
                                                                        && x.Id != Id);
                    if (validarTipoEmbargo != null)
                    {
                        errors.Add(new ValidationResult(
                            $"El concepto que intentas guardar ya existe para éste tipo de embargo.",
                            new[] { "ConceptoNominaId" }));
                    }

                    //Valida el concepto de nómina tipo de deducción
                    if (validaConceptoNomina.ClaseConceptoNomina != ClaseConceptoNomina.Devengo)
                    {
                        errors.Add(new ValidationResult(
                            $"El concepto debe ser de devengo.",
                            new[] { "ConceptoNominaId" }));
                    }
                }

                var validaTipoEmbargo = dbContexto.TipoEmbargos.FirstOrDefault(x => x.Id == TipoEmbargoId);
                if (validaTipoEmbargo == null)
                {
                    errors.Add(new ValidationResult(
                        $"No existe.",
                        new[] { "TipoEmbargoId" }));
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
