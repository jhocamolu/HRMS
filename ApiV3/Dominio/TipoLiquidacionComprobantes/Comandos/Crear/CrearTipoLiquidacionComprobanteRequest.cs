using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoLiquidacionComprobantes.Comandos.Crear
{
    public class CrearTipoLiquidacionComprobanteRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoLiquidacionId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(TipoComprobante), ErrorMessage = ConstantesErrores.FormatoDelEmunerador)]
        public TipoComprobante? TipoComprobante { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CentroCostoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CuentaContableId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(NaturalezaContable), ErrorMessage = ConstantesErrores.FormatoDelEmunerador)]
        public NaturalezaContable? Naturaleza { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaTipo = dbContexto.TipoLiquidaciones.FirstOrDefault(x => x.Id == TipoLiquidacionId &&
                                                                                x.EstadoRegistro == EstadoRegistro.Activo);
                if (validaTipo == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe tipo de liquidación con los datos ingresados.",
                        new[] { "TipoLiquidacionId" }));
                }

                var validaCentroCosto = dbContexto.CentroCostos.FirstOrDefault(x => x.Id == CentroCostoId &&
                                                                                x.EstadoRegistro == EstadoRegistro.Activo);
                if (validaCentroCosto == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe el centro de costo con los datos ingresados.",
                        new[] { "CentroCostoId" }));
                }
                var validaCuentaContable = dbContexto.CuentaContables.FirstOrDefault(x => x.Id == CuentaContableId &&
                                                                                x.EstadoRegistro == EstadoRegistro.Activo);
                if (validaCuentaContable == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe la cuenta contable con los datos ingresados.",
                        new[] { "CuentaContableId" }));
                }

                var cuentaContableNaturaleza = dbContexto.TipoLiquidacionComprobantes.FirstOrDefault(x => x.TipoComprobante == TipoComprobante &&
                                                                                                    x.Naturaleza == Naturaleza &&
                                                                                                    x.TipoLiquidacionId == TipoLiquidacionId &&
                                                                                                    x.EstadoRegistro == EstadoRegistro.Activo 
                                                                                                        );
                if (cuentaContableNaturaleza != null)
                {
                    errores.Add(new ValidationResult(
                       $"El parámetro contable que intentas ingresar ya se encuentra registrado para la misma naturaleza.",
                       new[] { "TipoLiquidacionId" }));
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
