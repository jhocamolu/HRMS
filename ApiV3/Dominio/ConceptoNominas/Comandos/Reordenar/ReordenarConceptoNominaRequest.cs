using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Reordenar
{
    public class ReordenarConceptoNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(CondicionConceptoNomina), ErrorMessage = "No es un tipo de condición para ordenar conceptos de nómina válido.")]
        public CondicionConceptoNomina? Condicion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ConceptoNominaId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                var existe = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult("No existe.",
                                              new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region ValidaIguales
                if (Id == ConceptoNominaId)
                {
                    errores.Add(new ValidationResult("El concepto seleccionado es el mismo concepto que deseas cambiar.",
                      new[] { "ConceptoNominaId" }));
                    return errores;
                }
                #endregion

                #region ConceptoNominaId
                var conceptoNomina = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId);
                if (conceptoNomina == null)
                {
                    errores.Add(new ValidationResult("No existe.",
                                              new[] { "ConceptoNominaId" }));
                    return errores;
                }
                #endregion

                // Definicion del orden nuevo
                int ordenNuevo = 0;
                if (Condicion == CondicionConceptoNomina.AntesDe)
                {
                    ordenNuevo = conceptoNomina.Orden;
                }
                else if (Condicion == CondicionConceptoNomina.DespuesDe)
                {
                    ordenNuevo = conceptoNomina.Orden + 1;
                }

                #region ValidacionesOrdenFormula


                // validacion para los conceptos que estan relacionados en su formula
                var validacionFormula = contexto.ConceptoNominaElementoFormulas.Where(x => x.ConceptoNominaId == Id && x.Tipo == TipoElementoFormula.Concepto).ToList();
                if (validacionFormula.Any())
                {
                    foreach (var item in validacionFormula)
                    {
                        var consulta = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == item.ElementoFormulaId);
                        if (ordenNuevo <= consulta.Orden)
                        {
                            errores.Add(new ValidationResult("No es posible asignar el orden definido al concepto ya que  afecta el funcionamiento de su fórmula.",
                                              new[] { "Snack" }));
                            return errores;
                        }
                    }
                }

                // validacion para cuando es un elemento de una formula de otro concepto
                var validacionElemento = contexto.ConceptoNominaElementoFormulas.Where(x => x.ElementoFormulaId == Id && x.Tipo == TipoElementoFormula.Concepto).ToList();
                if (validacionElemento.Any())
                {
                    foreach (var item in validacionElemento)
                    {
                        var consulta = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == item.ConceptoNominaId);
                        if (ordenNuevo >= consulta.Orden)
                        {
                            errores.Add(new ValidationResult("No es posible asignar el orden del concepto ya que afecta el funcionamiento de las fórmulas de los conceptos en donde está incluído.",
                                              new[] { "Snack" }));
                            return errores;
                        }
                    }
                }
                #endregion

                #region ValidacionesOrdenAgrupadores
                if (existe.ConceptoAgrupador)
                {
                    //consulta si es un concepto agrupador
                    var validacionAgrupador = contexto.ConceptoBases.Where(c => c.ConceptoNominaAgrupadorId == existe.Id).ToList();
                    if (validacionAgrupador.Any())
                    {
                        foreach (var item in validacionAgrupador)
                        {
                            var consulta = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == item.ConceptoNominaId);
                            if (ordenNuevo <= consulta.Orden)
                            {
                                errores.Add(new ValidationResult("No es posible asignar el orden definido al concepto ya que afecta la obtención de su valor.",
                                                  new[] { "Snack" }));
                                return errores;
                            }
                        }
                    }
                }
                else
                {
                    // consulta si es base para calcular el concepto agrupador
                    var validacionBase = contexto.ConceptoBases.Where(c => c.ConceptoNominaId == existe.Id).ToList();
                    if (validacionBase.Any())
                    {
                        foreach (var item in validacionBase)
                        {
                            var consulta = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == item.ConceptoNominaAgrupadorId);
                            if (ordenNuevo >= consulta.Orden)
                            {
                                errores.Add(new ValidationResult("No es posible asignar el orden definido al concepto ya que afecta la obtención de su valor.",
                                                  new[] { "Snack" }));
                                return errores;
                            }
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
    }
}
