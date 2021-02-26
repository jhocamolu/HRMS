using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Nominas.Comandos.Parcial
{
    public class ParcialNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoLiquidacionId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? SubperiodoId { get; set; }
        public int? PeriodoContableId { get; set; }
        public bool? Activo { get; set; }

        #endregion
        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                if (TipoLiquidacionId != null)
                {
                    var validaTipoLiquidacion = dbContexto.TipoLiquidaciones.FirstOrDefault(x => x.Id == TipoLiquidacionId);
                    if (validaTipoLiquidacion == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo liquidación"),
                           new[] { "TipoLiquidacionId" }));
                    }
                }
                if (SubperiodoId != null)
                {
                    var validaSubperiodo = dbContexto.SubPeriodos.FirstOrDefault(x => x.Id == SubperiodoId);
                    if (validaSubperiodo == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("sub período"),
                           new[] { "SubperiodoId" }));
                    }
                }
                if (PeriodoContableId != null)
                {
                    var validaPeriodoContable = dbContexto.PeriodoContables.FirstOrDefault(x => x.Id == PeriodoContableId && x.Estado == EstadoPeriodoContable.Activo);
                    if (validaPeriodoContable == null)
                    {
                        errores.Add(new ValidationResult("No es posible generar una liquidación hasta que exista un periodo contable activo",
                           new[] { "PeriodoContableId" }));
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
