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

namespace ApiV3.Dominio.Embargos.Comandos.Parcial
{
    public class ParcialEmbargosRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? JuzgadoId { get; set; }

        public int? FuncionarioId { get; set; }

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

        #region Estado Registro
        public bool? Activo { get; set; }
        #endregion

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
                    var validaPrioridad = contexto.Embargos
                                                  .FirstOrDefault(x => x.Prioridad == Prioridad &&
                                                                       x.TipoEmbargoId == TipoEmbargoId &&
                                                                       x.FuncionarioId == existeID.FuncionarioId &&
                                                                       x.Id != Id);
                    if (validaPrioridad != null)
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

                        var embargoCreadoSuperior = contexto.Embargos.Where(x => x.FuncionarioId == existeID.FuncionarioId &&
                                                                    x.Estado != EstadoEmbargo.Anulado &&
                                                                    x.Estado != EstadoEmbargo.Terminado &&
                                                                   x.Prioridad > Prioridad &&
                                                                   x.Id != Id
                                                                    )
                                                         .Include(x => x.TipoEmbargo)
                                                         .OrderByDescending(x => x.Prioridad)
                                                         .FirstOrDefault();

                        var embargoDelMismoTipo = contexto.Embargos.Where(x => x.FuncionarioId == existeID.FuncionarioId &&
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
