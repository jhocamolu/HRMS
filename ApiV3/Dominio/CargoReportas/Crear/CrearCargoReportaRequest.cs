using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CargoReportas.Crear
{
    public class CrearCargoReportaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoDependenciaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoDependenciaReportaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool JefeInmediato { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var existeCargoDependencia = dbContexto.CargoDependencias.FirstOrDefault(x => x.Id == CargoDependenciaId);
                if (CargoDependenciaId != null)
                {
                    //Valida CargoId exista 

                    if (existeCargoDependencia == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El cargo no existe.",
                            new[] { "CargoDependenciaId" }));
                    }
                }

                var existeMeReportan = dbContexto.CargoDependencias.FirstOrDefault(x => x.Id == CargoDependenciaReportaId);

                if (CargoDependenciaReportaId != null)
                {
                    //Valida CargoDependenciaReportaId exista

                    if (existeMeReportan == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El cargo reporta a no existe.",
                            new[] { "CargoDependenciaReportaId" }));
                    }
                }

                if (JefeInmediato == true)
                {

                    //Valida que no exista jefe inmediato agregado con anterioridad.
                    var validaJefeInmediato = dbContexto.CargoReportas.FirstOrDefault(x => x.CargoDependenciaId == CargoDependenciaId &&
                                                                                            x.CargoDependenciaReporta.DependenciaId == existeMeReportan.DependenciaId &&
                                                                                            x.JefeInmediato == true &&
                                                                                            x.EstadoRegistro == EstadoRegistro.Activo);
                    if (validaJefeInmediato != null)
                    {
                        errores.Add(new ValidationResult(
                            $"Ya existe un cargo al que reporta seleccionado como jefe inmediato para esta dependencia.",
                            new[] { "snack" }));
                    }


                }

                if (CargoDependenciaId != null && CargoDependenciaReportaId != null)
                {
                    //Valida CargoReporta Exista 
                    var existeCargoReporta = dbContexto.CargoReportas.FirstOrDefault(x => x.CargoDependenciaId == CargoDependenciaId &&
                                                                                          x.CargoDependenciaReportaId == CargoDependenciaReportaId &&
                                                                                          x.EstadoRegistro == EstadoRegistro.Activo);
                    if (existeCargoReporta != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El cargo al que reporta que intentas guardar ya existe.",
                            new[] { "CargoDependenciaReportaId" }));
                    }

                    //Valida que el cargoId  sea diferente de ReportaId
                    if (CargoDependenciaId == CargoDependenciaReportaId)
                    {
                        errores.Add(new ValidationResult(
                            $"El cargo al que reporta que intentas guardar es igual al cargo que estás modificando.",
                            new[] { "CargoDependenciaReportaId" }));
                    }
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
