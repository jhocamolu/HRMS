using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudVacaciones.Comandos.Estado
{
    public class EstadoSolicitudVacacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public EstadoSolicitudVacaciones? Estado { get; set; }

        public string Justificacion { get; set; }

        public DateTime? FechaFinDisfrute { get; set; }

        #endregion
        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                var solicitudPermiso = dbContexto.SolicitudVacaciones.FirstOrDefault(x => x.Id == Id);
                if (solicitudPermiso == null)
                {
                    errores.Add(new ValidationResult("No existe.",
                                              new[] { "Id" }));
                }
                #endregion

                #region Estado

                if (Estado == EstadoSolicitudVacaciones.Rechazada || Estado == EstadoSolicitudVacaciones.Anulada)
                {
                    if (string.IsNullOrEmpty(Justificacion))
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                                              new[] { "Justificacion" }));
                    }
                }

                if (Estado == EstadoSolicitudVacaciones.Terminada)
                {
                    if (FechaFinDisfrute == DateTime.MinValue)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                                              new[] { "FechaFinDisfrute" }));
                    }
                    else if ((DateTime)FechaFinDisfrute.Value.Date < DateTime.Now.Date)
                    {
                        errores.Add(new ValidationResult("La fecha de terminación no puede ser menor a la fecha actual.",
                                              new[] { "FechaFinDisfrute" }));
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

