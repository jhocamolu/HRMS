using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Libranzas.Comandos.Estado
{
    public class EstadoLibranzaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public EstadoLibranza? Estado { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Justificacion { get; set; }

        #endregion
        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                var libranza = dbContexto.Libranzas.FirstOrDefault(x => x.Id == Id);
                if (libranza == null)
                {
                    errores.Add(new ValidationResult("No existe.",
                                              new[] { "Id" }));
                }
                #endregion

                #region Estado

                if (Estado != null)
                {
                    if (string.IsNullOrEmpty(Justificacion))
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                                              new[] { "Justificacion" }));
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
