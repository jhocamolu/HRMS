using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.AusentismoFuncionarios.Comandos.Crear
{

    public class CrearAusentismoFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        public int? ProrrogaId { get; set; }

        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoAusentismoId { get; set; }

        public int? DiagnosticoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime FechaFin { get; set; }

        public DateTime? FechaIniciaReal { get; set; }
        public DateTime? FechaFinalReal { get; set; }
        
        public TimeSpan? HoraInicio { get; set; }

        public TimeSpan? HoraFin { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string NumeroIncapacidad { get; set; }

        public string Adjunto { get; set; }

        public string Observacion { get; set; }
        #endregion
        #region Validaciones Manuales


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                /// Ausentismo 
                #region Verifica ya tiene registro esa prorroga
                if (ProrrogaId != null)
                {
                    var validaProrroga = dbContexto.ProrrogaAusentismos.FirstOrDefault(x => x.ProrrogaId == ProrrogaId && x.EstadoRegistro == EstadoRegistro.Activo);
                    if (validaProrroga != null)
                    {
                        errores.Add(new ValidationResult(
                         $"Ya se encuentra prorrogada el ausentismo seleccionado.",
                         new[] { "ProrrogaId" }));
                    }
                    else
                    {
                        var consultaProrroga = dbContexto.AusentismoFuncionarios.FirstOrDefault(x => x.Id == ProrrogaId && x.FuncionarioId == FuncionarioId);
                        var diaAnterior = FechaInicio.AddDays(-1);
                        if (consultaProrroga != null)
                        {
                            //La fecha de inicio debe tener continuidad con la fecha de finalización del asentismo anterior
                            if (diaAnterior.Date != consultaProrroga.FechaFin.Date)
                            {
                                errores.Add(new ValidationResult("No puedes prorrogar la incapacidad porque no hay continuidad en las fechas.",
                                    new[] { "ProrrogaId" }));
                            }
                        }
                        else
                        {
                            errores.Add(new ValidationResult("No existe.",
                                    new[] { "ProrrogaId" }));
                        }
                    }
                }

                #endregion

                #region AusentismoFuncionario

                var validaFechaInicioEntre = dbContexto.AusentismoFuncionarios.FirstOrDefault(x => x.FuncionarioId == FuncionarioId
                                                                                                    && x.Estado != EstadoAusentismo.Anulado
                                                                                                    && ((x.FechaInicio <= FechaInicio
                                                                                                    && x.FechaFin >= FechaInicio) ||
                                                                                                (x.FechaInicio <= FechaFin
                                                                                                    && x.FechaFin >= FechaFin)
                                                                                                ));
                if (validaFechaInicioEntre != null)
                {
                    errores.Add(new ValidationResult(
                         $"No se puede guardar el ausentismo, ya que existe un ausentismo con las fechas ingresadas.",
                         new[] { "Snack" }));
                }
                #endregion

                #region TipoAusentismo
                var validaTipoAusentismo = dbContexto.TipoAusentismos.FirstOrDefault(x => x.Id == TipoAusentismoId);
                if (validaTipoAusentismo == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe",
                        new[] { "TipoAusentismoId" }));
                }
                #endregion
                #region DiagnosticoCIE
                if (DiagnosticoId != null)
                {
                    var validaDiagnostico = dbContexto.DiagnosticoCies.FirstOrDefault(x => x.Id == DiagnosticoId);
                    if (validaDiagnostico == null)
                    {
                        errores.Add(new ValidationResult(
                            $"No existe",
                            new[] { "Diagnostico" }));
                    }
                }
                #endregion
                #region FuncionarioId
                var validaFuncionario = dbContexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                if (validaFuncionario == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe",
                        new[] { "FuncionarioId" }));
                }
                else
                {
                    if (validaFuncionario.Estado == EstadoFuncionario.Retirado || validaFuncionario.Estado == EstadoFuncionario.Seleccionado)
                    {
                        errores.Add(new ValidationResult(
                        $"El funcionario debe estar en estado “Activo” o “Vacaciones”.",
                        new[] { "FuncionarioId" }));
                    }
                }
                #endregion
                #region Fecha Fin
                DateTime fechaVacia = DateTime.MinValue;
                if (FechaFin != fechaVacia)
                {
                    if (FechaFin < FechaInicio)
                    {
                        errores.Add(new ValidationResult("La fecha final que intentas guardar no puede ser menor que la fecha inicial.",
                            new[] { "FechaFin" }));
                    }
                }
                else
                {
                    errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaFin" }));
                }
                #endregion
                #region Fecha Inicial
                if (FechaInicio != fechaVacia)
                {
                    DateTime hoy = DateTime.Today;
                    if (FechaInicio < hoy.AddYears(-1))
                    {
                        errores.Add(new ValidationResult("La fecha inicial no debe ser menor a un año.",
                            new[] { "FechaInicio" }));
                    }
                    if (FechaInicio > hoy.AddDays(15))
                    {
                        errores.Add(new ValidationResult("La fecha inicial no puede ser mayor a la fecha actual más de 15 días.",
                            new[] { "FechaInicio" }));
                    }
                }
                else
                {
                    errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaInicio" }));
                }


                #endregion


                if (validaTipoAusentismo != null)
                {
                    var claseAusentismo = dbContexto.ClaseAusentismos.FirstOrDefault(x => x.Id == validaTipoAusentismo.ClaseAusentismoId);
                    if (claseAusentismo.Codigo != "LNR" && validaFuncionario.Estado == EstadoFuncionario.EnVacaciones && validaTipoAusentismo.Codigo != "LPL")
                    {
                        errores.Add(new ValidationResult(
                            $"No puedes guardar un ausentismo de clase “Licencia remunerada” a un funcionario en estado “En vacaciones”.",
                            new[] { "TipoAusentismoId" }));
                    }

                    if (claseAusentismo.Codigo == "LNR" && validaFuncionario.Estado == EstadoFuncionario.EnVacaciones)
                    {
                        errores.Add(new ValidationResult(
                            $"No puedes guardar un ausentismo de clase “Licencia no remunerada” a un funcionario en estado “En vacaciones”.",
                            new[] { "TipoAusentismoId" }));
                    }

                    if (claseAusentismo.RequiereHora)
                    {
                        #region HoraInicio
                        TimeSpan horaVacia = TimeSpan.Zero;
                        if (HoraInicio == horaVacia)
                        {
                            errores.Add(new ValidationResult("Requerido",
                                new[] { "HoraInicio" }));
                        }
                        #endregion
                        #region HoraFin

                        if (HoraFin != horaVacia)
                        {
                            if (HoraFin < HoraInicio && FechaInicio == FechaFin)
                            {
                                errores.Add(new ValidationResult("La hora final que intentas guardar no puede ser menor que la hora inicial.",
                                    new[] { "HoraFin" }));
                            }
                        }
                        else
                        {
                            errores.Add(new ValidationResult("Requerido",
                            new[] { "HoraFin" }));
                        }
                        #endregion
                    }
                }

                #region  FechaIniciaReal
                if (FechaIniciaReal != null &&
                    FechaFinalReal == null)
                {
                    errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaFinalReal" }));
                }

                if (FechaIniciaReal == null &&
                    FechaFinalReal != null)
                {
                    errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaIniciaReal" }));
                }

                if (FechaIniciaReal != fechaVacia &&
                    FechaFinalReal != fechaVacia &&
                    FechaIniciaReal != null &&
                    FechaFinalReal != null)
                {
                    if (FechaFinalReal < FechaIniciaReal)
                    {
                        errores.Add(new ValidationResult("La fecha final real que intentas guardar no puede ser menor que la fecha inicial real.",
                            new[] { "FechaFinalReal" }));
                    }

                    DateTime hoy = DateTime.Today;
                    if (FechaIniciaReal < hoy.AddYears(-1))
                    {
                        errores.Add(new ValidationResult("La fecha inicial real no debe ser menor a un año.",
                            new[] { "FechaIniciaReal" }));
                    }
                    if (FechaIniciaReal > hoy.AddDays(15))
                    {
                        errores.Add(new ValidationResult("La fecha inicial real no puede ser mayor a la fecha actual más de 15 días.",
                            new[] { "FechaIniciaReal" }));
                    }
                }
                #endregion

            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message,
                           new[] { "Excepcion" }));
            }

            return errores;
        }
        #endregion
    }
}