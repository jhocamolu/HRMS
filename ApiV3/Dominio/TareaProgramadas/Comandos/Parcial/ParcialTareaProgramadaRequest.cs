using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Parcial
{
    public class ParcialTareaProgramadaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Alias { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool EnEjecucion { get; set; }

        #endregion

        #region Validaiones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                if (Id == null && Alias == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tarea programada"), new[] { "Alias" }));
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tarea programada"), new[] { "Id" }));
                    return errores;
                }
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                if (Id != null)
                {
                    var tareaprogramada = contexto.TareaProgramadas.FirstOrDefault(x => x.Id == Id);
                    if (tareaprogramada == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tarea programada"), new[] { "Id" }));
                    }
                }

                if (Alias != null)
                {
                    var tareaprogramada = contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == Alias);
                    if (tareaprogramada == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tarea programada"), new[] { "Alias" }));
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
