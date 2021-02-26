using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoAusentismoConceptoNominas.Comandos.Crear
{
    public class CrearTipoAusentismoConceptoNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoAusentismoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ConceptoNominaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 1, maximum: 9999, ErrorMessage = ConstantesErrores.Rango + "1 a 9999.")]
        public int? CoberturaDesde { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 1, maximum: 9999, ErrorMessage = ConstantesErrores.Rango + "1 a 9999.")]
        public int? CoberturaHasta { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {

                //Valida que código sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region TipoAusentismoId
                var tipoAusentismo = dbContexto.TipoAusentismos.FirstOrDefault(x => x.Id == TipoAusentismoId);
                if (tipoAusentismo == null)
                {
                    errores.Add(new ValidationResult(
                        $"El tipo de ausentismo que intentas guardar no existe.",
                        new[] { "TipoAusentismoId" }));
                }
                #endregion

                #region ConceptoNominaId
                var conceptoNomina = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId);
                if (conceptoNomina == null)
                {
                    errores.Add(new ValidationResult(
                        $"El concepto de nómina que intentas guardar no existe.",
                        new[] { "ConceptoNominaId" }));
                }
                #endregion

                #region Coberturas
                if (CoberturaDesde >= CoberturaHasta)
                {
                    errores.Add(new ValidationResult(
                        $"La cobertura desde no puede ser mayor que la cobertura hasta.",
                        new[] { "CoberturaHasta" }));
                }

                var elemento = dbContexto.TipoAusentismoConceptoNominas.FirstOrDefault(x =>
                                                        x.TipoAusentismoId == TipoAusentismoId &&
                                                        //x.ConceptoNominaId == ConceptoNominaId &&
                                                        x.CoberturaDesde == CoberturaDesde &&
                                                        x.CoberturaHasta == CoberturaHasta);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"Ya existe un concepto que comprende la cobertura en días que intentas guardar.",
                        new[] { "ConceptoNominaId" }));
                }

                elemento = dbContexto.TipoAusentismoConceptoNominas.FirstOrDefault(x =>
                                            x.TipoAusentismoId == TipoAusentismoId &&
                                           //x.ConceptoNominaId == ConceptoNominaId &&
                                           (
                                           (x.CoberturaDesde <= CoberturaDesde && CoberturaDesde <= x.CoberturaHasta) ||
                                           (x.CoberturaDesde <= CoberturaHasta && CoberturaHasta <= x.CoberturaHasta) ||
                                           (CoberturaDesde <= x.CoberturaDesde && x.CoberturaDesde <= CoberturaHasta) ||
                                           (CoberturaHasta <= x.CoberturaHasta && x.CoberturaHasta <= CoberturaHasta)
                                           )
                                          );
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"Ya existe un concepto que comprende uno de los dos campos de las coberturas en días que intentas guardar.",
                        new[] { "CoberturaDesde", "CoberturaHasta" }));
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
