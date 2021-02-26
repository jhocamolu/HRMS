using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realziar las validaciones de formato para actualizaciones
/// parciales de la entidad NivelEducativo
/// </summary>

namespace ApiV3.Dominio.NivelEducativos.Comandos.Parcial
{
    public class ParcialNivelEducativoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region Nombre
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + "60.")]
        public string Nombre { get; set; }
        #endregion

        #region Orden
        public int? Orden { get; set; }
        #endregion

        #region Estado Registro
        public bool? Activo { get; set; }
        #endregion

        #endregion

        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                var id = contexto.NivelEducativos.FirstOrDefault(x => x.Id == Id);
                if (id == null)
                {
                    errores.Add(new ValidationResult("No existe Id.", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region Nombre
                var nombre = contexto.NivelEducativos.FirstOrDefault(x => x.Id != Id && x.Nombre == Nombre);
                if (nombre != null)
                {
                    errores.Add(new ValidationResult("El nivel educativo que intentas actualizar ya existe",
                       new[] { "Nombre" }));
                }
                #endregion

                #region Orden
                if (Orden != null)
                {
                    var ordenNiveleEducativo = contexto.NivelEducativos.FirstOrDefault(x => x.Orden == Orden && x.Id != Id);
                    if (ordenNiveleEducativo != null)
                    {
                        errores.Add(new ValidationResult("Ya existe un nivel educativo con este orden. Por favor revisa.", new[] { "Orden" }));
                    }
                    else if (Orden <= 0 || Orden >= 100)
                    {
                        errores.Add(new ValidationResult("El orden debe estar entre 1 y 99.", new[] { "Orden" }));
                    }
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
