using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.LiquidacionConceptos.Comandos.Crear
{
    public class CrearTipoLiquidacionConceptoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoliquidacionId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ConceptoNominaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoContratoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? SubPeriodoId { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region TipoliquidacionId
                var existeTipoliquidacion = contexto.TipoLiquidaciones.FirstOrDefault(x => x.Id == TipoliquidacionId);
                if (existeTipoliquidacion == null)
                {
                    errores.Add(new ValidationResult(
                       $"El tipo de liquidación que intentas guardar no existe.",
                       new[] { "TipoliquidacionId" }));
                }
                #endregion

                #region ConceptoNominaId
                var existeConceptoNomina = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId);
                if (existeConceptoNomina == null)
                {
                    errores.Add(new ValidationResult(
                       $"El concepto de nómina que intentas guardar no existe.",
                       new[] { "ConceptoNominaId" }));

                    return errores;
                }
                #endregion

                #region SubPeriodoId
                var existeSubPeriodo = contexto.SubPeriodos.FirstOrDefault(x => x.Id == SubPeriodoId);
                if (existeSubPeriodo == null)
                {
                    errores.Add(new ValidationResult(
                       $"El subperíodo que intentas guardar no existe.",
                       new[] { "SubPeriodoId" }));

                    return errores;
                }
                #endregion

                #region TipoContratoId
                if (TipoContratoId == 0)
                {
                    var existeConseptoTipoContratoNull = contexto.TipoLiquidacionConceptos
                                                        .FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId &&
                                                                             x.SubPeriodoId == SubPeriodoId &&
                                                                             x.TipoContratoId == null
                                                        );
                    if (existeConseptoTipoContratoNull != null)
                    {
                        errores.Add(new ValidationResult(
                            "El concepto de nómina que intentas ingresar ya se encuentra ingresado para todos los tipos de contrato en este subperíodo.",
                            new[] { "Snack" }));
                    }


                    var existeConseptoTipoContratoConCodigo = contexto.TipoLiquidacionConceptos
                                                        .FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId &&
                                                                             x.SubPeriodoId == SubPeriodoId &&
                                                                             x.TipoContratoId != null
                                                                        );

                    if (existeConseptoTipoContratoConCodigo != null)
                    {
                        errores.Add(new ValidationResult(
                            "El concepto de nómina que intentas ingresar ya se encuentra ingresado en otro concepto para este tipo de contrato en este subperíodo.",
                            new[] { "Snack" }));
                    }
                }
                else
                {
                    var existeTipoContrato = contexto.TipoContratos.FirstOrDefault(x => x.Id == TipoContratoId);
                    if (existeTipoContrato == null)
                    {
                        errores.Add(new ValidationResult(
                           $"El tipo de contrato que intentas guardar no existe.",
                           new[] { "TipoContratoId" }));

                    }
                    else
                    {
                        var existeConseptoTipoContratoConCodigo = contexto.TipoLiquidacionConceptos
                                                       .FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId &&
                                                                            x.SubPeriodoId == SubPeriodoId &&
                                                                            x.TipoContratoId == TipoContratoId
                                                       );
                        if (existeConseptoTipoContratoConCodigo != null)
                        {
                            errores.Add(new ValidationResult(
                                "El concepto de nómina que intentas ingresar ya existe para este tipo de contrato y subperíodo.",
                                new[] { "Snack" }));
                        }



                        var existeConseptoTipoContratoNull = contexto.TipoLiquidacionConceptos
                                                       .FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId &&
                                                                            x.SubPeriodoId == SubPeriodoId &&
                                                                            x.TipoContratoId == null
                                                       );
                        if (existeConseptoTipoContratoNull != null)
                        {
                            errores.Add(new ValidationResult(
                                "El concepto de nómina que intentas ingresar ya se encuentra ingresado para todos los tipos de contrato en este subperíodo.",
                                new[] { "Snack" }));
                        }
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
