using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SoporteSolicitudPermisos.Comandos.Crear
{
    public class CrearSoporteSolicitudPermisoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? SolicitudPermisoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoSoporteId { get; set; }

        public string Comentario { get; set; }

        public string Adjunto { get; set; }

        #endregion
        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                // Valida solicitud permiso
                var validaSolicitud = dbContexto.SolicitudPermisos.FirstOrDefault(x => x.Id == SolicitudPermisoId);
                if (validaSolicitud == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("solicitud permiso."),
                           new[] { "SolicitudPermisoId" }));
                }

                // valida tipo soporte
                var validaTipoSoporte = dbContexto.TipoSoportes.FirstOrDefault(x => x.Id == TipoSoporteId);
                if (validaTipoSoporte == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo soporte."),
                           new[] { "TipoSoporteId" }));
                }

                //Consulta el tipo soporte ingresado
                var validaSoporte = dbContexto.SoporteSolicitudPermisos
                                              .FirstOrDefault(x => x.SolicitudPermisoId == SolicitudPermisoId &&
                                                                  x.TipoSoporteId == TipoSoporteId);
                if (validaSoporte != null)
                {
                    errores.Add(new ValidationResult("El tipo de soporte que intentas guardar, ya está asociado a la solicitud de permiso.",
                           new[] { "TipoSoporteId" }));
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