using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Actualizar
{
    public class ActualizarTareaProgramadaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico +
                                   ConstantesExpresionesRegulares.Espacio + "]*$",
                          ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Periodicidad { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Instruccion { get; set; }

        #endregion

        #region Validaciones Manuel
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var tareaprogramada = contexto.TareaProgramadas.FirstOrDefault(x => x.Id == Id);
                if (tareaprogramada == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tarea programada"), new[] { "Id" }));
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
