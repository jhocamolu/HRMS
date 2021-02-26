using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudPermisos.Comandos.Crear
{
    public class CrearSolicitudPermisoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoAusentismoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaFin { get; set; }

        public TimeSpan? HoraSalida { get; set; }

        public TimeSpan? HoraLlegada { get; set; }

        public string Observaciones { get; set; }

        #endregion
        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {

                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                if (FuncionarioId != null)
                {
                    var validaFuncionario = dbContexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                    if (validaFuncionario == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario."),
                           new[] { "FuncionarioId" }));
                    }
                }
                #region  TipoAusentismoId
                if (TipoAusentismoId != null)
                {
                    var validaTipoAusentismo = dbContexto.TipoAusentismos.FirstOrDefault(x => x.Id == TipoAusentismoId);
                    if (validaTipoAusentismo == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo de permiso."),
                           new[] { "TipoAusentismoId" }));
                    }
                }
                #endregion

                #region  FechaInicio - FechaFin
                if (FechaInicio < DateTime.Today)
                {
                    errores.Add(new ValidationResult("La fecha de inicio no puede ser menor que la fecha actual.",
                            new[] { "FechaInicio" }));
                }
                if (FechaFin < FechaInicio)
                {
                    errores.Add(new ValidationResult("La fecha fin no puede ser menor que la fecha de inicio.",
                            new[] { "FechaFin" }));
                }
                #endregion
                #region ValidaFechas

                var validaFechas = dbContexto.SolicitudPermisos.Where(x => x.FuncionarioId == FuncionarioId &&
                                                                      x.Estado != EstadoSolicitudPermiso.Cancelada &&
                                                                      x.Estado != EstadoSolicitudPermiso.Rechazada &&
                                                                      x.EstadoRegistro == EstadoRegistro.Activo)
                                                                .ToList();
                var banderaError1 = false;
                var banderaError2 = false;
                var banderaError3 = false;
                if (validaFechas != null)
                {
                    foreach (var validaFecha in validaFechas)
                    {
                        if ((validaFecha.FechaInicio <= FechaInicio && FechaInicio <= validaFecha.FechaFin) &&
                            (validaFecha.FechaInicio <= FechaFin && FechaFin <= validaFecha.FechaFin))
                        {
                            banderaError1 = true;
                        }
                        if ((FechaInicio < validaFecha.FechaInicio && validaFecha.FechaInicio < FechaFin) &&
                            (FechaInicio < validaFecha.FechaFin && validaFecha.FechaFin < FechaFin))
                        {
                            banderaError2 = true;
                        }
                        if ((FechaInicio == validaFecha.FechaInicio && FechaFin != validaFecha.FechaFin) ||
                            (FechaInicio != validaFecha.FechaInicio && FechaFin == validaFecha.FechaFin))
                        {
                            banderaError3 = true;
                        }
                    }
                }
                if (banderaError1 == true)
                {
                    errores.Add(new ValidationResult("Las fechas de la solicitud son iguales, o están entre las fechas de otra solicitud de permiso asociada al funcionario.",
                            new[] { "snackbar" }));
                }
                if (banderaError2 == true)
                {
                    errores.Add(new ValidationResult("Las fechas de la solicitud contienen las fechas de otra solicitud de permiso asociada al funcionario.",
                            new[] { "snackbar" }));
                }
                if (banderaError3 == true && banderaError1 != true)
                {
                    errores.Add(new ValidationResult("Una de las dos fechas de la solicitud es igual, o está  entre las fechas de otra solicitud de permiso asociada al funcionario.",
                            new[] { "snackbar" }));
                }
                #endregion
                #region Hora
                if (HoraSalida != null && HoraLlegada != null)
                {
                    if (HoraLlegada <= HoraSalida)
                    {
                        errores.Add(new ValidationResult("La hora de llegada no puede ser igual o menor a la hora de salida.",
                           new[] { "HoraLlegada" }));
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
