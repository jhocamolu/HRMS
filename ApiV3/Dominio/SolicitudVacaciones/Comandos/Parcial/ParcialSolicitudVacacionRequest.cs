using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudVacaciones.Comandos.Parcial
{
    public class ParcialSolicitudVacacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? Id { get; set; }

        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }

        public int? LibroVacacionesId { get; set; }

        public DateTime? FechaInicioDisfrute { get; set; }

        [Range(1, 99, ErrorMessage = ConstantesErrores.Rango + "1 - 99.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? DiasDisfrute { get; set; }

        public DateTime FechaFinDisfrute { get; set; }

        [Range(0, 99, ErrorMessage = ConstantesErrores.Rango + "0 - 99.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? DiasDinero { get; set; }

        public string Observacion { get; set; }

        #endregion
        #region ValidacionManual

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Existe registro
                var validaExiste = dbContexto.SolicitudVacaciones.FirstOrDefault(x => x.Id == Id);
                if (validaExiste == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("solicitud de vacaciones."),
                       new[] { "Id" }));
                }
                else
                {
                    if (validaExiste.FechaInicioDisfrute > FechaInicioDisfrute)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio no puede ser menor a la fecha que ingresaste cuando creaste la solicitud de vacaciones.",
                           new[] { "FechaInicioDisfrute" }));
                    }
                }
                #endregion
                #region Funcionario
                if (FuncionarioId != null)
                {
                    var validaFuncionario = dbContexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId && x.Estado == EstadoFuncionario.Activo);
                    if (validaFuncionario == null)
                    {
                        errores.Add(new ValidationResult("El funcionario que intentas ingresar no se encuentra activo. Por favor revisa.",
                           new[] { "FuncionarioId" }));
                    }
                }
                #endregion
                #region libroVacaciones
                if (LibroVacacionesId != null)
                {
                    var validaLibroVacaciones = dbContexto.LibroVacaciones.FirstOrDefault(x => x.Id == LibroVacacionesId);
                    if (validaLibroVacaciones == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("libro vacaciones."),
                           new[] { "LibroVacacionesId" }));
                    }
                    else
                    {
                        //Consulta si el periodo de vacaciones tiene más de 360  y 15 días de distfrute
                        if (validaLibroVacaciones.DiasDisponibles == 15 && DiasDisfrute < 6)
                        {
                            errores.Add(new ValidationResult("No puedes registrar menos de 6 días de disfrute por 360 días trabajados.",
                                                        new[] { "DiasDisfrute" }));
                        }

                        if ((validaLibroVacaciones.Tipo == TipoLibroVacaciones.Causado) &&
                            ((DiasDisfrute + DiasDinero) > validaLibroVacaciones.DiasDisponibles))
                        {
                            errores.Add(new ValidationResult("Los días registrados en la solicitud no pueden ser mayor a los días disponibles.",
                                                        new[] { "snackbar" }));
                        }
                        int porcentajeDiasDisponibles = (int)validaLibroVacaciones.DiasDisponibles / 2;
                        if (DiasDinero > porcentajeDiasDisponibles)
                        {
                            errores.Add(new ValidationResult("No puedes guardar más del 50% de los días disponibles en el campo días en dinero.",
                                                        new[] { "DiasDinero" }));
                        }
                    }
                }
                #endregion

                #region FechaInicioDisfrute
                if (FechaInicioDisfrute != DateTime.MinValue)
                {
                    // valida si es día hábil.
                    if (DiaHabil.ValidaDiaHabil((DateTime)FechaInicioDisfrute, dbContexto) != true)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio que intentas guardar debe corresponder a un día hábil.",
                              new[] { "FechaInicioDisfrute" }));
                    }

                    // Calcular fecha fin a partir de la fecha de inicio y los dias a disfrutar (días habiles).
                    FechaFinDisfrute = DiaHabil.SumaDiasHabilesFecha((DateTime)FechaInicioDisfrute, (int)DiasDisfrute, dbContexto);
                }
                #endregion

                #region ValidaFechas
                if (FechaFinDisfrute != DateTime.MinValue)
                {

                    var validaFechas = dbContexto.SolicitudVacaciones.Where(x => x.FuncionarioId == FuncionarioId &&
                                                                          x.Estado != EstadoSolicitudVacaciones.Cancelada &&
                                                                          x.Estado != EstadoSolicitudVacaciones.Rechazada &&
                                                                          x.Estado != EstadoSolicitudVacaciones.Terminada &&
                                                                          x.Estado != EstadoSolicitudVacaciones.Anulada &&
                                                                          x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                          x.Id != Id)
                                                                    .ToList();
                    var banderaError1 = false;
                    var banderaError2 = false;
                    var banderaError3 = false;
                    if (validaFechas != null)
                    {
                        foreach (var validaFecha in validaFechas)
                        {
                            if ((validaFecha.FechaInicioDisfrute <= FechaInicioDisfrute && FechaInicioDisfrute <= validaFecha.FechaFinDisfrute) &&
                                (validaFecha.FechaInicioDisfrute <= FechaFinDisfrute && FechaFinDisfrute <= validaFecha.FechaFinDisfrute))
                            {
                                banderaError1 = true;
                            }
                            if ((FechaInicioDisfrute < validaFecha.FechaInicioDisfrute && validaFecha.FechaInicioDisfrute < FechaFinDisfrute) &&
                                (FechaInicioDisfrute < validaFecha.FechaFinDisfrute && validaFecha.FechaFinDisfrute < FechaFinDisfrute))
                            {
                                banderaError2 = true;
                            }
                            if ((FechaInicioDisfrute == validaFecha.FechaInicioDisfrute && FechaFinDisfrute != validaFecha.FechaFinDisfrute) ||
                                (FechaInicioDisfrute != validaFecha.FechaInicioDisfrute && FechaFinDisfrute == validaFecha.FechaFinDisfrute))
                            {
                                banderaError3 = true;
                            }
                        }
                    }
                    if (banderaError1 == true)
                    {
                        errores.Add(new ValidationResult("Las fechas del período a disfrutar son iguales, o están entre las fechas de otra solicitud de vacaciones asociada al funcionario.",
                                new[] { "snackbar" }));
                    }
                    if (banderaError2 == true)
                    {
                        errores.Add(new ValidationResult("Las fechas del período a disfrutar contienen las fechas de otra solicitud de vacaciones asociada al funcionario.",
                                new[] { "snackbar" }));
                    }
                    if (banderaError3 == true && banderaError1 != true)
                    {
                        errores.Add(new ValidationResult("Una de las dos fechas del período a disfrutar es igual, o está entre las fechas de otra solicitud de vacaciones asociada al funcionario.",
                                new[] { "snackbar" }));
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
