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

namespace ApiV3.Dominio.Novedades.Comandos.Crear
{
    public class CrearNovedadRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CategoriaNovedadId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaAplicacion { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoPeriodoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(UnidadMedida), ErrorMessage = "No es una unidad valida.")]
        public UnidadMedida? Unidad { get; set; }

        [Range(0, 100000000, ErrorMessage = ConstantesErrores.Rango + "0 - 100000000.")]
        public decimal? Valor { get; set; }

        [Range(0, 100000, ErrorMessage = ConstantesErrores.Rango + "0 - 100000.")]
        public double? Cantidad { get; set; }

        public int? TerceroId { get; set; }

        public string Observacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public List<int> Periodicidad { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var funcionario = contexto.FuncionarioDatoActuales.Include(c => c.Contrato).FirstOrDefault(x => x.Id == FuncionarioId);

                if (funcionario == null)
                {
                    errores.Add(new ValidationResult(
                      $"{ConstantesErrores.NoExiste("el funcionario")}",
                      new[] { "FuncionarioId" }));
                }
                else
                {
                    if (funcionario.Estado != EstadoFuncionario.Activo || funcionario.Contrato.Estado != EstadoContrato.Vigente)
                    {
                        errores.Add(new ValidationResult(
                      $"El funcionario que intentas ingresar no se encuentra activo, o no tiene contrato vigente, por favor revisa.",
                      new[] { "FuncionarioId" }));
                    }
                }

                var categoriaNovedad = contexto.CategoriaNovedades.Include(c => c.ConceptoNomina).FirstOrDefault(c => c.Id == CategoriaNovedadId);
                if (categoriaNovedad == null)
                {
                    errores.Add(new ValidationResult(
                      $"{ConstantesErrores.NoExiste("la categoria de novedad")}",
                      new[] { "CategoriaNovedadId" }));
                }

                if (categoriaNovedad != null)
                {
                    if (categoriaNovedad.RequiereTercero && TerceroId == null)
                    {
                        errores.Add(new ValidationResult(
                                 $"{ConstantesErrores.Requerido}",
                                 new[] { "TerceroId" }));
                    }
                    else if (categoriaNovedad.RequiereTercero && TerceroId != null)
                    {
                        switch (categoriaNovedad.UbicacionTercero)
                        {
                            case UbicacionTerceroCategoriaNovedad.Administradora:
                                var administradora = contexto.Administradoras.FirstOrDefault(x => x.Id == TerceroId);
                                if (administradora == null)
                                {
                                    errores.Add(new ValidationResult(
                                     $"{ConstantesErrores.NoExiste("el tercero")}",
                                     new[] { "TerceroId" }));
                                }
                                break;
                            case UbicacionTerceroCategoriaNovedad.EntidadFinanciera:
                                var entidad = contexto.EntidadFinancieras.FirstOrDefault(x => x.Id == TerceroId);
                                if (entidad == null)
                                {
                                    errores.Add(new ValidationResult(
                                     $"{ConstantesErrores.NoExiste("el tercero")}",
                                     new[] { "TerceroId" }));
                                }
                                break;
                            case UbicacionTerceroCategoriaNovedad.OtrosTerceros:
                                var tercero = contexto.Terceros.FirstOrDefault(x => x.Id == TerceroId);
                                if (tercero == null)
                                {
                                    errores.Add(new ValidationResult(
                                     $"{ConstantesErrores.NoExiste("el tercero")}",
                                     new[] { "TerceroId" }));
                                }
                                break;
                            default:
                                errores.Add(new ValidationResult(
                                     $"{ConstantesErrores.NoExiste("el tercero")}",
                                     new[] { "TerceroId" }));
                                break;
                        }
                    }

                    if (Unidad != categoriaNovedad.ConceptoNomina.UnidadMedida)
                    {
                        errores.Add(new ValidationResult(
                     $"La unidad que intentas ingresar no corresponde con la unidad del concepto de nomina relacionado.",
                     new[] { "Unidad" }));
                    }

                    if (categoriaNovedad.ConceptoNomina.RequiereCantidad)
                    {
                        if (Cantidad == null)
                        {
                            errores.Add(new ValidationResult(
                                     $"{ConstantesErrores.Requerido}",
                                     new[] { "Cantidad" }));
                        }
                    }
                    else
                    {
                        if (Valor == null)
                        {
                            errores.Add(new ValidationResult(
                                     $"{ConstantesErrores.Requerido}",
                                     new[] { "Valor" }));
                        }
                    }

                }

                if ((DateTime)FechaAplicacion > DateTime.Now)
                {
                    errores.Add(new ValidationResult(
                     $"La fecha que intentas ingresar no debe ser mayor que la fecha actual.",
                     new[] { "FechaAplicacion" }));
                }
                if (FechaFinalizacion != null && (DateTime)FechaFinalizacion < (DateTime)FechaAplicacion)
                {
                    errores.Add(new ValidationResult(
                     $"La fecha de finalización que intentas ingresar no debe ser menor que la fecha de la novedad.",
                     new[] { "FechaFinalizacion" }));
                }

                var tipoPeriodo = contexto.TipoPeriodos.FirstOrDefault(x => x.Id == TipoPeriodoId && x.PagoPorDefecto == true);
                if (tipoPeriodo == null)
                {
                    errores.Add(new ValidationResult(
                     $"{ConstantesErrores.NoExiste("el tipo de periodo")}",
                     new[] { "TipoPeriodoId" }));
                }

                if (Periodicidad.Any())
                {
                    var subperiodos = contexto.SubPeriodos.Where(x => x.TipoPeriodoId == TipoPeriodoId).ToList();
                    foreach (var item in Periodicidad)
                    {
                        var valida = subperiodos.FirstOrDefault(s => s.Id == item);
                        if (valida == null)
                        {
                            errores.Add(new ValidationResult(
                        $"Uno de los subperiodos seleccionados no esta relacionado en el tipo de periodo que indico.",
                        new[] { "Periodicidad" }));
                            return errores;
                        }
                    }
                }
                else
                {
                    errores.Add(new ValidationResult(
                                     $"{ConstantesErrores.Requerido}",
                                     new[] { "Periodicidad" }));
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
