using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Nominas.Comandos.Actualizar
{
    public class ActualizarNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoLiquidacionId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? SubperiodoId { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFinal { get; set; }
        #endregion
        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaSubperiodo = dbContexto.SubPeriodos.FirstOrDefault(x => x.Id == SubperiodoId
                                                                            && x.EstadoRegistro == EstadoRegistro.Activo);
                if (validaSubperiodo == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("sub período"),
                       new[] { "SubperiodoId" }));
                }

                var validaTipoLiquidacion = dbContexto.TipoLiquidaciones.FirstOrDefault(x => x.Id == TipoLiquidacionId
                                                                                        && x.EstadoRegistro == EstadoRegistro.Activo);
                if (validaTipoLiquidacion == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo liquidación"),
                       new[] { "TipoLiquidacionId" }));
                }
                else
                {
                    if (validaTipoLiquidacion.FechaManual == true)
                    {
                        if (FechaInicio == null)
                        {
                            errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaInicio" }));
                        }

                        if (FechaFinal == null)
                        {
                            errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaFinal" }));
                        }

                        if (FechaInicio != null && FechaFinal != null)
                        {
                            if (FechaFinal < FechaInicio)
                            {
                                errores.Add(new ValidationResult("La fecha final no puede ser menor que la fecha de inicio.",
                                new[] { "FechaFinal" }));
                            }

                            TimeSpan t = (DateTime)FechaFinal - (DateTime)FechaInicio;
                            double nroDeDias = t.TotalDays;
                            if (nroDeDias < validaSubperiodo.Dias)
                            {
                                errores.Add(new ValidationResult("La cantidad de días entre la fecha inicial y la fecha final no cumplen con el tipo de liquidación seleccionado.",
                                new[] { "snack" }));
                            }
                        }

                    }
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
