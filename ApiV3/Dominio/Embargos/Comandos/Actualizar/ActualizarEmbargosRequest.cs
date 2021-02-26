using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static ApiV3.Infraestructura.Utilidades.DigitoVerificacion;

namespace ApiV3.Dominio.Embargos.Comandos.Actualizar
{
    public class EmbargosSubperiodo
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? SubperiodoId { get; set; }

        public int? EmbargoId { get; set; }
    }
    public class EmbargosConceptoNomina
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? ConceptoNominaId { get; set; }
    }
    public class ActualizarEmbargosRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? JuzgadoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoEmbargoId { get; set; }

        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        public string NumeroProceso { get; set; }

        [Range(minimum: 1, maximum: 999999999.99, ErrorMessage = ConstantesErrores.Rango + "$1 a $999.999.999,99")]
        public double? ValorEmbargo { get; set; }

        [Range(minimum: 1, maximum: 999999999.99, ErrorMessage = ConstantesErrores.Rango + "$1 a $999.999.999,99")]
        public double? ValorCuota { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 1, maximum: 99, ErrorMessage = ConstantesErrores.Rango + "1 a 99.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? Prioridad { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? EntidadFinancieraId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(30, ErrorMessage = ConstantesErrores.Maximo + "30.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string NumeroCuenta { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + "15.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string NumeroDocumentoDemandante { get; set; }

        [Range(0, 9, ErrorMessage = ConstantesErrores.Rango + "0 - 9.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? DigitoVerificacionDemandante { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Demandante { get; set; }


        [Range(1.00, 100.00, ErrorMessage = ConstantesErrores.Rango + "1 - 100.")]
        public decimal? PorcentajeCuota { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public EstadoEmbargo? Estado { get; set; }

        public bool? ActualizaPrioridad { get; set; }

        public bool? PrioridadSumaOResta { get; set; }

        //lista Subperiodos Embargo
        public List<EmbargosSubperiodo> EmbargosSubperiodo { get; set; } = new List<EmbargosSubperiodo>();

        //lista de Conceptos de Nómina
        public List<EmbargosConceptoNomina> EmbargosConceptoNomina { get; set; } = new List<EmbargosConceptoNomina>();
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region  ExisteID
                var existeID = contexto.Embargos.FirstOrDefault(x => x.Id == Id);
                if (existeID == null)
                {
                    errores.Add(new ValidationResult("No existe Id", new[] { "Id" }));
                    return errores;
                }
                #endregion
                if (existeID.Estado == EstadoEmbargo.Anulado &&
                    existeID.Estado == EstadoEmbargo.Terminado
                    )
                {
                    errores.Add(new ValidationResult("No se puede actualizar el embargo con estado anulado o terminado.",
                        new[] { "snackbar" }));
                }
                #region DigitoVerificacionDemandante
                if (DigitoVerificacionDemandante != null)
                {
                    if (DigitoVerificacionDemandante.ToString() != CalcularDigitoVerificacion(NumeroDocumentoDemandante.ToString()))
                    {
                        errores.Add(new ValidationResult("El dígito verificación que intentas guardar no es correcto",
                            new[] { "DigitoVerificacionDemandante" }));
                    }
                }
                #endregion

                #region NumeroProceso
                if (!string.IsNullOrEmpty(NumeroProceso))
                {
                    var numeroProceso = contexto.Embargos
                                                .FirstOrDefault(x => x.NumeroProceso == NumeroProceso &&
                                                                     x.Id != Id);
                    if (numeroProceso != null)
                    {
                        errores.Add(new ValidationResult("El número de proceso que intentas guardar ya está relacionado a otro embargo.",
                            new[] { "NumeroProceso" }));
                    }
                }
                #endregion

                #region FuncionarioId
                if (FuncionarioId != null)
                {
                    var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                    if (funcionario == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionarios"),
                          new[] { "FuncionarioId" }));
                    }
                }
                #endregion

                #region JuzgadoId
                if (JuzgadoId != null)
                {
                    var juzgado = contexto.Juzgados.FirstOrDefault(x => x.Id == JuzgadoId);
                    if (juzgado == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("juzgado"),
                          new[] { "JuzgadoId" }));
                    }
                }
                #endregion

                #region TipoEmbargoId
                if (TipoEmbargoId != null)
                {
                    var tipoEmbargo = contexto.TipoEmbargos.FirstOrDefault(x => x.Id == TipoEmbargoId);
                    if (tipoEmbargo == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo embargo"),
                          new[] { "TipoEmbargoId" }));
                    }
                }
                #endregion

                #region EntidadFinancieraId
                if (EntidadFinancieraId != null)
                {
                    var entidadFinanciera = contexto.EntidadFinancieras.FirstOrDefault(x => x.Id == EntidadFinancieraId);
                    if (entidadFinanciera == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("entidad finaciera"),
                          new[] { "EntidadFinancieraId" }));
                    }
                }
                #endregion

                #region Prioridad ActualizaPrioridad
                if (Prioridad != null)
                {


                    var tipoEmbargo = contexto.TipoEmbargos.FirstOrDefault(x => x.Id == TipoEmbargoId);

                    var embargoCreado = contexto.Embargos.Where(x => (x.FuncionarioId == FuncionarioId &&
                                                                x.Estado != EstadoEmbargo.Anulado &&
                                                                x.Estado != EstadoEmbargo.Terminado &&
                                                                x.Prioridad <= Prioridad &&
                                                                x.Id != Id)
                                                                ||
                                                                (x.FuncionarioId == FuncionarioId &&
                                                                x.Estado != EstadoEmbargo.Anulado &&
                                                                x.Estado != EstadoEmbargo.Terminado &&
                                                                x.Id != Id &&
                                                                x.Prioridad == Prioridad &&
                                                                x.TipoEmbargoId != TipoEmbargoId)
                                                                )
                                                     .Include(x => x.TipoEmbargo)
                                                     .OrderByDescending(x => x.Prioridad)
                                                     .FirstOrDefault();

                    var embargoCreadoSuperior = contexto.Embargos.Where(x => x.FuncionarioId == FuncionarioId &&
                                                                x.Estado != EstadoEmbargo.Anulado &&
                                                                x.Estado != EstadoEmbargo.Terminado &&
                                                               x.Prioridad > Prioridad &&
                                                               x.Id != Id
                                                                )
                                                     .Include(x => x.TipoEmbargo)
                                                     .OrderByDescending(x => x.Prioridad)
                                                     .FirstOrDefault();

                    var embargoDelMismoTipo = contexto.Embargos.Where(x => x.FuncionarioId == FuncionarioId &&
                                                                x.Estado != EstadoEmbargo.Anulado &&
                                                                x.Estado != EstadoEmbargo.Terminado &&
                                                               x.TipoEmbargoId == TipoEmbargoId &&
                                                               x.Id != Id
                                                                )
                                                     .Include(x => x.TipoEmbargo)
                                                     .OrderByDescending(x => x.Prioridad)
                                                     .FirstOrDefault();

                    var maxNumeroMismoTipo = 0;
                    if (embargoDelMismoTipo != null)
                    {
                        maxNumeroMismoTipo = embargoDelMismoTipo.Prioridad + 1;
                    }

                    //Validaciones de prioridad: Los tipos de embargo tienen prioridad, (menor prioridad más importancia)
                    // Embargo también tiene prioridad.
                    if (embargoCreado != null)
                    {
                        if (embargoCreado.TipoEmbargo.Prioridad > tipoEmbargo.Prioridad)
                        {
                            if (embargoCreado.Prioridad >= Prioridad)
                            {
                                if (maxNumeroMismoTipo == 0 || maxNumeroMismoTipo == Prioridad)
                                {
                                    if (ActualizaPrioridad != true)
                                    {
                                        errores.Add(new ValidationResult($"La prioridad de los embargos registrados con anterioridad de tipo {embargoCreado.TipoEmbargo.Nombre.ToLower()} se actualizaron, ya que el embargo de tipo {tipoEmbargo.Nombre.ToLower()} tiene prelación frente a estos.",
                                        new[] { "ActualizaPrioridad" }));
                                    }
                                }
                                else
                                {
                                    errores.Add(new ValidationResult($"La prioridad del embargo de tipo {tipoEmbargo.Nombre.ToLower()} no puede ser un número igual o menor a la prioridad de un embargo de tipo {embargoCreado.TipoEmbargo.Nombre.ToLower()}. Por favor revisa.",
                                       new[] { "ActualizaPrioridad" }));
                                }
                            }
                            else if (embargoCreado.Prioridad < Prioridad)
                            {
                                errores.Add(new ValidationResult($"La prioridad del embargo de tipo {tipoEmbargo.Nombre.ToLower()} no puede ser un número igual o menor a la prioridad de un embargo de tipo {embargoCreado.TipoEmbargo.Nombre.ToLower()}. Por favor revisa.",
                                       new[] { "ActualizaPrioridad" }));
                            }
                        }
                        if (embargoCreado.TipoEmbargo.Prioridad < tipoEmbargo.Prioridad)
                        {
                            if (embargoCreado.Prioridad >= Prioridad)
                            {
                                errores.Add(new ValidationResult($"La prioridad del embargo de tipo {tipoEmbargo.Nombre.ToLower()} no puede ser un número igual o menor a la prioridad de un embargo de tipo {embargoCreado.TipoEmbargo.Nombre.ToLower()}. Por favor revisa.",
                                       new[] { "ActualizaPrioridad" }));
                            }
                        }
                        if (embargoCreado.TipoEmbargo.Prioridad == tipoEmbargo.Prioridad)
                        {
                            if (embargoCreado.Prioridad >= Prioridad)
                            {
                                if (ActualizaPrioridad != true)
                                {
                                    errores.Add(new ValidationResult($"Ya existe un embargo de tipo {tipoEmbargo.Nombre.ToLower()} con prioridad No.{Prioridad}. Si guardas este embargo, los demás asociados al funcionario se actualizarán. ¿Estás seguro de continuar?",
                                   new[] { "ActualizaPrioridad" }));
                                }
                            }
                            if (embargoCreado.Prioridad < Prioridad && embargoCreadoSuperior != null)
                            {
                                if (ActualizaPrioridad != true)
                                {
                                    errores.Add(new ValidationResult($"Ya existe un embargo de tipo {tipoEmbargo.Nombre.ToLower()} con prioridad No.{Prioridad}. Si guardas este embargo, los demás asociados al funcionario se actualizarán. ¿Estás seguro de continuar?",
                                   new[] { "ActualizaPrioridad" }));
                                }
                            }
                        }
                    }

                }
                #endregion

                #region FechaFin FechaFin

                if (FechaInicio == null && FechaFin != null)
                {
                    errores.Add(new ValidationResult("Si ingresas la fecha fin, se requiere la fecha inicio.",
                        new[] { "FechaInicio" }));
                }
                if (FechaFin != null && FechaInicio != null)
                {
                    if (FechaFin < FechaInicio)
                    {
                        errores.Add(new ValidationResult("La fecha fin que intentas guardar no puede ser menor que la fecha inicio.",
                            new[] { "FechaFin" }));
                    }
                }
                #endregion

                #region EmbargosSubperiodoLista
                if (EmbargosSubperiodo.Count() != 0)
                {
                    foreach (var item in EmbargosSubperiodo)
                    {
                        var subperiodo = contexto.SubPeriodos.FirstOrDefault(x => x.Id == item.SubperiodoId);
                        if (subperiodo == null)
                        {
                            errores.Add(new ValidationResult($"No es un subperíodo válido.",
                                new[] { "EmbargosSubperiodo" }));
                        }
                    }

                }
                else
                {
                    errores.Add(new ValidationResult("Requerido",
                                new[] { "EmbargosSubperiodo" }));
                }
                #endregion
                #region EmbargosConceptoNomina Lista
                if (EmbargosConceptoNomina.Count() != 0)
                {
                    double minimoPorcentajeDescuento = 0;
                    foreach (var item in EmbargosConceptoNomina)
                    {
                        var conceptoNomina = contexto.TipoEmbargoConceptoNominas
                                                .FirstOrDefault(x => x.ConceptoNominaId == item.ConceptoNominaId &&
                                                                x.ConceptoNomina.ClaseConceptoNomina == ClaseConceptoNomina.Devengo &&
                                                                x.TipoEmbargoId == TipoEmbargoId);
                        if (conceptoNomina == null)
                        {
                            errores.Add(new ValidationResult($"No es un concepto de nómina válido.",
                                new[] { "EmbargosConceptoNomina" }));
                        }
                        else
                        {
                            // Obtiene el minimo porcentaje de descuento entre los conceptos de nómina.
                            if (minimoPorcentajeDescuento == 0)
                            {
                                minimoPorcentajeDescuento = conceptoNomina.MaximoEmbargarConcepto;
                            }
                            if (conceptoNomina.MaximoEmbargarConcepto < minimoPorcentajeDescuento)
                            {
                                minimoPorcentajeDescuento = conceptoNomina.MaximoEmbargarConcepto;
                            }
                        }
                    }
                    if (PorcentajeCuota != null && minimoPorcentajeDescuento != 0)
                    {
                        if (PorcentajeCuota > (decimal)minimoPorcentajeDescuento)
                        {
                            errores.Add(new ValidationResult($"El porcentaje de la cuota excede el porcentaje máximo a embargar del concepto a embargar seleccionado.",
                                new[] { "snackbar" }));
                        }
                    }
                }
                else
                {
                    errores.Add(new ValidationResult("Requerido",
                                new[] { "EmbargosConceptoNomina" }));
                }
                #endregion
                #region Valor Cuota
                if (ValorCuota != null)
                {
                    if (ValorCuota > ValorEmbargo)
                    {
                        errores.Add(new ValidationResult($"El valor de la cuota no puede ser mayor que el valor del embargo.",
                                    new[] { "ValorCuota" }));

                    }
                }
                #endregion
                #region Valor Cuota y porcentaje
                if (ValorCuota == null && PorcentajeCuota == null)
                {
                    errores.Add(new ValidationResult($"No podrás guardar el embargo, si no ingresas el valor de la cuota o el porcentaje de la cuota.",
                                new[] { "snackbar" }));
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
