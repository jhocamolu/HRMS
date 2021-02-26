using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ActividadCentroCostos.comandos.Actualizar
{
    public class ActualizarActividadCentroCostoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ActividadId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CentroCostoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? MunicipioId { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Existe 
                if (ActividadId != null)
                {
                    var validaActividad = dbContexto.Actividades.FirstOrDefault(x => x.Id == ActividadId);
                    if (validaActividad == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("actividad"),
                           new[] { "ActividadId" }));
                    }
                }
                if (CentroCostoId != null)
                {
                    var validaCentroCosto = dbContexto.CentroCostos.FirstOrDefault(x => x.Id == CentroCostoId);
                    if (validaCentroCosto == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro costo"),
                           new[] { "CentroCostoId" }));
                    }
                }
                if (MunicipioId != null)
                {
                    var validaMunicipio = dbContexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == MunicipioId);
                    if (validaMunicipio == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("municipio"),
                           new[] { "MunicipioId" }));
                    }
                }
                #endregion
                #region Centro Costo
                var repiteCentroCosto = dbContexto.ActividadCentroCostos.FirstOrDefault(x => x.MunicipioId == MunicipioId &&
                                                                                    x.CentroCostoId == CentroCostoId &&
                                                                                    x.Id != Id);
                if (repiteCentroCosto != null)
                {
                    errores.Add(new ValidationResult("El centro de costo que intentas ingresar ya existe para alguna actividad registrada.",
                       new[] { "snack" }));
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