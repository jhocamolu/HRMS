using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar las validaciones para  actualizaciones parciales del modelo JornadaLaborales
/// </summary>

namespace ApiV3.Dominio.JornadaLaborales.Comandos.Parcial
{
    public class ParcialJornadaLaboralRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }


        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        public string Nombre { get; set; }


        public bool? Activo { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                var existe = contexto.JornadaLaborales.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult($"No existe", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region Nombre
                var nombre = contexto.JornadaLaborales.FirstOrDefault(x => x.Id != Id && x.Nombre == Nombre);
                if (nombre != null)
                {
                    errores.Add(new ValidationResult("El nombre de la jornada laboral que intentas guardar ya existe.",
                        new[] { "Nombre" }));
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
