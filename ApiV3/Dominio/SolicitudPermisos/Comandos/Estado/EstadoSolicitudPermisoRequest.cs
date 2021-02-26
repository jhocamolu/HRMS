using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudPermisos.Comandos.Estado
{
    public class EstadoSolicitudPermisoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public EstadoSolicitudPermiso? Estado { get; set; }

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
                var solicitudPermiso = dbContexto.SolicitudPermisos.FirstOrDefault(x => x.Id == Id);
                if (solicitudPermiso == null)
                {
                    errores.Add(new ValidationResult("No existe.",
                                              new[] { "Id" }));
                }
                else
                {
                    if (solicitudPermiso.Estado == EstadoSolicitudPermiso.Cancelada ||
                        solicitudPermiso.Estado == EstadoSolicitudPermiso.Autorizada ||
                        solicitudPermiso.Estado == EstadoSolicitudPermiso.Rechazada)
                    {
                        errores.Add(new ValidationResult("No se puede cambiar el estado de esta solicitud.",
                                              new[] { "snackbar" }));
                    }
                    else if (
                        (solicitudPermiso.Estado == EstadoSolicitudPermiso.Aprobada
                        && (Estado != EstadoSolicitudPermiso.Autorizada && Estado != EstadoSolicitudPermiso.Rechazada))
                        ||
                        (solicitudPermiso.Estado == EstadoSolicitudPermiso.Solicitada
                        && (Estado != EstadoSolicitudPermiso.Cancelada && Estado != EstadoSolicitudPermiso.Aprobada && Estado != EstadoSolicitudPermiso.Rechazada))
                        )
                    {
                        errores.Add(new ValidationResult("No se puede cambiar el estado de esta solicitud.",
                                              new[] { "snackbar" }));
                    }

                }
                #endregion

                #region Estado

                if (Estado == EstadoSolicitudPermiso.Rechazada)
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
