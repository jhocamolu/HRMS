using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TareaProgramadaLogs.Commandos.Crear
{
    public class CrearTareaProgramadaLogRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string TareaProgramadaAlias { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(EstadoTareaProgramada))]
        public EstadoTareaProgramada? Estado { get; set; }

        public string Resultado { get; set; }
        #endregion


        #region Validaciones Manuel
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var tareaprogramada = contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == TareaProgramadaAlias);
                if (tareaprogramada == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tarea programada"), new[] { "TareaProgramadaAlias" }));
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
