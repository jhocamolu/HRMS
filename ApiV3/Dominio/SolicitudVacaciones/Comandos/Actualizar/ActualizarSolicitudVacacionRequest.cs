using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudVacaciones.Comandos.Actualizar
{
    public class ActualizarSolicitudVacacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? LibroVacacionesId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicioDisfrute { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
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
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("solicitud de vacaciones"),
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
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("libro vacaciones"),
                           new[] { "LibroVacacionesId" }));
                    }
                    else
                    {
                        if ((validaLibroVacaciones.Tipo == TipoLibroVacaciones.Causado) &&
                            ((DiasDisfrute + DiasDinero) > validaLibroVacaciones.DiasDisponibles))
                        {
                            errores.Add(new ValidationResult("Los días registrados en la solicitud no pueden ser mayor a los días disponibles.",
                                                        new[] { "snackbar" }));
                        }

                        // Consulta cantidad de solicitudes para el periodo
                        var solicitudesRealizadas = dbContexto.SolicitudVacaciones.Where(x => x.LibroVacacionesId == LibroVacacionesId
                                                                                        && x.Estado != EstadoSolicitudVacaciones.Anulada
                                                                                        && x.Estado != EstadoSolicitudVacaciones.Cancelada
                                                                                        && x.Estado != EstadoSolicitudVacaciones.Rechazada
                                                                                        )
                                                                                        .ToList();

                        if ((validaLibroVacaciones.Tipo == TipoLibroVacaciones.Causado) && solicitudesRealizadas.Count == 0)
                        {
                            int porcentajeDiasDisponibles = (int)validaLibroVacaciones.DiasDisponibles / 2;
                            /***CA16 Comentariado, pendinete ajusres crietrio
                           if (DiasDinero > porcentajeDiasDisponibles)
                           {
                               errores.Add(new ValidationResult("No puedes pagar más del 50% de los días disponibles en dinero.",
                                                           new[] { "DiasDinero" }));
                           }
                           ***/
                            if (DiasDinero != null && DiasDinero != 0)
                            {
                                if ((DiasDinero + DiasDisfrute) != (int)validaLibroVacaciones.DiasDisponibles)
                                {
                                    errores.Add(new ValidationResult("Para pagar días en dinero debes tomar todos los días disponibles del período.",
                                                                new[] { "snackbar" }));
                                }


                            }
                        }
                        else
                        {
                            bool banderaEstado = false;
                            foreach (var item in solicitudesRealizadas)
                            {
                                if (item.Estado == EstadoSolicitudVacaciones.Terminada)
                                {
                                    banderaEstado = true;
                                }
                            }
                            if (((DiasDisfrute + DiasDinero) < validaLibroVacaciones.DiasDisponibles) &&
                                (banderaEstado = true && DiasDinero != null && DiasDinero != 0))
                            {
                                errores.Add(new ValidationResult("No puedes pagar días en dinero en este período de vacaciones. Por favor revisa.",
                                                                new[] { "snackbar" }));
                            }
                        }

                        if (validaLibroVacaciones.Tipo == TipoLibroVacaciones.Anticipado)
                        {
                            if (DiasDinero != null && DiasDinero != 0)
                            {
                                errores.Add(new ValidationResult("No puedes pagar días en dinero en un anticipo de vacaciones.",
                                                            new[] { "DiasDinero" }));
                            }
                        }
                    }
                }
                #endregion

                #region FechaInicioDisfrute
                DateTime actual = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime primerDiaDelMes = new DateTime(actual.Year, actual.Month, 1);
                /***CA06 Comentariado para pruebas
                if (FechaInicioDisfrute != validaExiste.FechaInicioDisfrute && (DateTime)FechaInicioDisfrute.Value.Date < primerDiaDelMes.Date)
                {
                    errores.Add(new ValidationResult("La fecha de inicio no puede ser diferente al mes en curso o inferior.",
                            new[] { "FechaInicioDisfrute" }));
                }
                ***/
                if (FechaInicioDisfrute != DateTime.MinValue)
                {
                    // valida si es día hábil.
                    /***CA07 Comentariado para pruebas
                    if (DiaHabil.ValidaDiaHabil((DateTime)FechaInicioDisfrute, dbContexto) != true)
                    {
                        errores.Add(new ValidationResult("La fecha de inicio que intentas guardar debe corresponder a un día hábil.",
                              new[] { "FechaInicioDisfrute" }));
                    }
                    **/
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
