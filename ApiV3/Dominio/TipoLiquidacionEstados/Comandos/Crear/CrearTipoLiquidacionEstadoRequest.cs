using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoLiquidacionEstados.Comandos.Crear
{
    public class CrearTipoLiquidacionEstadoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoLiquidacionId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(EstadoFuncionario), ErrorMessage = "No es un estado valido.")]
        public EstadoFuncionario? EstadoFuncionario { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(EstadoContrato), ErrorMessage = "No es un estado valido.")]
        public EstadoContrato? EstadoContrato { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region TipoliquidacionId
                var existeTipoliquidacion = contexto.TipoLiquidaciones
                                                    .FirstOrDefault(x => x.Id == TipoLiquidacionId);
                if (existeTipoliquidacion == null)
                {
                    errores.Add(new ValidationResult(
                       $"El tipo de liquidación que intentas guardar no existe.",
                       new[] { "TipoLiquidacionId" }));

                    return errores;
                }

                var validacionEstados = contexto.TipoLiquidacionEstados
                                                .FirstOrDefault(x => x.TipoLiquidacionId == TipoLiquidacionId &&
                                                                     x.EstadoContrato == EstadoContrato &&
                                                                     x.EstadoFuncionario == EstadoFuncionario
                                                 );
                if (validacionEstados != null)
                {
                    errores.Add(new ValidationResult(
                       $"El estado del funcionario que intentas ingresar ya existe para este estado de contrato.",
                       new[] { "Snack" }));
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
