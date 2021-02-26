using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Terceros.Comandos.Parcial
{
    public class ParcialTerceroRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? Activo { get; set; }
        #endregion

        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            NominaDbContext contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            try
            {
                if (Id != null)
                {
                    Tercero existeId = contexto.Terceros.FirstOrDefault(x => x.Id == Id);
                    if (existeId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("Tercero"),
                            new[] { "snack" }));
                        return errores;
                    }
                }
            }
            catch (System.Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
        #endregion
    }
}
