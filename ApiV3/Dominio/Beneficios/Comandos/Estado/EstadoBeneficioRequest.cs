using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Beneficios.Comandos.Estado
{
    public class EstadoBeneficioRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                #region Id
                var existeId = contexto.Beneficios.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("beneficio"),
                        new[] { "Id" }));
                    return errores;
                }
                else if (existeId.Estado != EstadoBeneficiosCorportativos.EnTramite &&
                    existeId.EstadoRegistro == EstadoRegistro.Activo)
                {
                    errores.Add(new ValidationResult("La solicitud no puede ser modificada en el estado en el que se encuentra.",
                        new[] { "confirmarError" }));
                    return errores;
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
