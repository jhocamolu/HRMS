using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de eliminar registros de la entidad NivelEducativo
/// </summary>

namespace ApiV3.Dominio.NivelEducativos.Comandos.Crear
{
    public class CrearNivelEducativoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + "60.")]
        public string Nombre { get; set; }
        #endregion

        #region Orden
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Orden { get; set; }
        #endregion

        #endregion

        #region Validaciones Manueales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Nombre
                var nivelEducativo = contexto.NivelEducativos.FirstOrDefault(x => x.Nombre == Nombre);
                if (nivelEducativo != null)
                {
                    errores.Add(new ValidationResult("El nivel educativo que intentas guardar ya existe.", new[] { "Nombre" }));
                }
                #endregion

                #region Orden
                var ordenNiveleEducativo = contexto.NivelEducativos.FirstOrDefault(x => x.Orden == Orden);
                if (ordenNiveleEducativo != null)
                {
                    errores.Add(new ValidationResult("Ya existe un nivel educativo con este orden. Por favor revisa.", new[] { "Orden" }));
                }
                else if (Orden <= 0 || Orden >= 100)
                {
                    errores.Add(new ValidationResult("El orden debe estar entre 1 y 99.", new[] { "Orden" }));
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
