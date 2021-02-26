using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.AnnoTrabajos.Comandos.Crear
{
    public class CrearAnnoVigenciaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Anno { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                // Valida año
                var validaAnno = dbContexto.AnnoVigencias.FirstOrDefault(x => x.Anno == Anno &&
                                                                        x.EstadoRegistro == EstadoRegistro.Activo);


                if (validaAnno != null)
                {
                    errores.Add(new ValidationResult(
                        $"El año de trabajo que intentas guardar ya existe.",
                        new[] { "Anno" }));

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
