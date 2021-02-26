using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validaciones para  crear registros en el modelo JornadaLaborales
/// </summary>

namespace ApiV3.Dominio.JornadaLaborales.Comandos.Crear
{
    public class CrearJornadaLaboralRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        public string Nombre { get; set; }
        #endregion

        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var nombre = contexto.JornadaLaborales.FirstOrDefault(x => x.Nombre == Nombre);
                if (nombre != null)
                {
                    errores.Add(new ValidationResult("El nombre de la jornada laboral que intentas guardar ya existe.",
                        new[] { "Nombre" }));
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
