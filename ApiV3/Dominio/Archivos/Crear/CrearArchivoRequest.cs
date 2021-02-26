using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Archivos.Crear
{
    public class CrearArchivoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Archivo { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                #region Archivo
                if (Archivo == null)
                {
                    errores.Add(new ValidationResult("Requerido", new[] { "Archivo" }));
                }
                else if (Archivo.Length % 4 != 0 ||
                   Archivo.Contains(' ') || Archivo.Contains('\t') || Archivo.Contains('\r') || Archivo.Contains('\n'))
                {
                    errores.Add(new ValidationResult("No es un formato válido para la conversión.", new[] { "Archivo" }));
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
