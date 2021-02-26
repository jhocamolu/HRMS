using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoLiquidacionEstados.Comandos.Eliminar
{
    public class EliminarTipoLiquidacionEstadoRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                var existe = contexto.TipoLiquidacionEstados.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No existe",
                       new[] { "Id" }));
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
