using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Sustitutos.Comandos.Parcial
{
    public class ParcialSustitutoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        public int Id { get; set; }

        public int? CargoASustituirId { get; set; }

        public int? CentroOperativoASutituirId { get; set; }

        public int? CargoSustitutoId { get; set; }

        public int? CentroOperativoSustitutoId { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFinal { get; set; }

        public string Observacion { get; set; }

        public EstadoRegistro? Activo { get; set; }

        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region existe
                var validaSustituto = contexto.Sustitutos.FirstOrDefault(x => x.Id == Id);
                if (FechaInicio != null)
                {
                    if (validaSustituto.FechaInicio > FechaInicio)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio no puede ser menor a la fecha que ingresaste cuando registraste el reemplazo.",
                           new[] { "FechaInicio" }));
                    }
                }
                #endregion
                #region Cargo A Sustituir
                if (CargoASustituirId != null)
                {
                    var validaCargoASustituir = contexto.Cargos.FirstOrDefault(x => x.Id == CargoASustituirId);
                    if (validaCargoASustituir == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cargo"),
                           new[] { "CargoASustituirId" }));
                    }
                    else
                    {
                        if (CentroOperativoASutituirId == null && validaCargoASustituir.Clase == ClaseCargo.CentroOperativo)
                        {
                            errores.Add(new ValidationResult("Requerido",
                           new[] { "CentroOperativoASutituirId" }));
                        }

                        if (CargoSustitutoId == CargoASustituirId
                        && CentroOperativoASutituirId != null
                        && CentroOperativoSustitutoId != null
                        && CentroOperativoASutituirId == CentroOperativoSustitutoId)
                        {
                            errores.Add(new ValidationResult("El cargo sustituto debe ser diferente al cargo a sustituir.",
                            new[] { "snackbarError" }));
                        }
                    }
                }
                #endregion
                #region Cargo A CargoSustituto
                if (CargoSustitutoId != null)
                {
                    var validaCargoSustituto = contexto.Cargos.FirstOrDefault(x => x.Id == CargoSustitutoId);
                    if (validaCargoSustituto == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cargo"),
                           new[] { "CargoSustitutoId" }));
                    }
                    else
                    {
                        if (CentroOperativoSustitutoId == null && validaCargoSustituto.Clase == ClaseCargo.CentroOperativo)
                        {
                            errores.Add(new ValidationResult("Requerido",
                           new[] { "CentroOperativoASutituirId" }));
                        }
                    }
                }
                #endregion
                #region  CentroOperativoASustituir
                if (CentroOperativoASutituirId != null)
                {
                    var validaCentroOperativoASustituir = contexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoASutituirId);
                    if (validaCentroOperativoASustituir == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro operativo"),
                            new[] { "CentroOperativoASutituirId" }));
                    }
                }
                #endregion

                #region  CentroOperativoSustituto
                if (CentroOperativoSustitutoId != null)
                {
                    var validaCentroOperativoSustituto = contexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoSustitutoId);
                    if (validaCentroOperativoSustituto == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("centro operativo"),
                            new[] { "CentroOperativoSustitutoId" }));
                    }
                }
                #endregion

                #region FechaFinal
                if (FechaFinal != DateTime.MinValue && FechaInicio != DateTime.MinValue)
                {
                    if (FechaFinal <= FechaInicio)
                    {
                        errores.Add(new ValidationResult("La fecha final no puede ser menor o igual a la fecha inicial.",
                                   new[] { "FechaFinal" }));
                    }
                }
                #endregion
                #region Fechas de registro iguales 
                if (CargoASustituirId != null)
                {

                    var validaFechas = contexto.Sustitutos.Where(x => x.CargoASustituirId == CargoASustituirId &&
                                                                      x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                      x.Id != Id
                                                                      )
                                                                        .ToList();
                    var banderaError = false;

                    if (validaFechas != null)
                    {
                        foreach (var validaFecha in validaFechas)
                        {
                            if ((validaFecha.FechaInicio <= FechaInicio && FechaInicio <= validaFecha.FechaFinal) ||
                            (validaFecha.FechaInicio <= FechaFinal && FechaFinal <= validaFecha.FechaFinal))
                            {
                                banderaError = true;
                            }
                            if ((validaFecha.FechaInicio <= FechaInicio && FechaInicio <= validaFecha.FechaFinal) &&
                                (validaFecha.FechaInicio <= FechaFinal && FechaFinal <= validaFecha.FechaFinal))
                            {
                                banderaError = true;
                            }
                            if ((FechaInicio < validaFecha.FechaInicio && validaFecha.FechaInicio < FechaFinal) &&
                                (FechaInicio < validaFecha.FechaFinal && validaFecha.FechaFinal < FechaFinal))
                            {
                                banderaError = true;
                            }
                            if ((FechaInicio == validaFecha.FechaInicio && FechaFinal != validaFecha.FechaFinal) ||
                                (FechaInicio != validaFecha.FechaInicio && FechaFinal == validaFecha.FechaFinal))
                            {
                                banderaError = true;
                            }
                        }
                    }
                    if (banderaError == true)
                    {
                        errores.Add(new ValidationResult("El cargo a sustituir que intentas guardar ya se le ha asignado un cargo sustituto. Por favor revisa.",
                                new[] { "snackbarAdvertencia" }));
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
