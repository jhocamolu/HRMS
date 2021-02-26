using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.JornadaLaboralDias.Comandos.Parcial
{
    public class ParcialJornadaLaboralDiaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public int? JornadaLaboralId { get; set; }


        [EnumDataType(typeof(DiaSemana), ErrorMessage = ConstantesErrores.FormatoDelEmunerador)]
        public DiaSemana? Dia { get; set; }

        #region HorarioLaboral
        public TimeSpan? HoraInicioJornada { get; set; }


        public TimeSpan? HoraFinJornada { get; set; }
        #endregion

        #region HorarioDescanso
        public TimeSpan? HoraInicioDescanso { get; set; }



        public TimeSpan? HoraFinDescanso { get; set; }

        #endregion


        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                var existe = contexto.JornadaLaboralDias.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult($"No existe", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region jornadaLaboralId
                var jornada = contexto.JornadaLaborales
                    .FirstOrDefault(x => x.Id == JornadaLaboralId);
                if (jornada == null)
                {
                    errores.Add(new ValidationResult("La jornadaLaboral no existe.",
                        new[] { "jornadaLaboralId" }));
                    return errores;
                }
                #endregion

                #region Dia
                if (Dia != null)
                {
                    if (JornadaLaboralId != null)
                    {
                        var dia = contexto.JornadaLaboralDias.FirstOrDefault(x => x.Id != Id &&
                                                                                  x.Dia == Dia &&
                                                                                  x.JornadaLaboralId == JornadaLaboralId);
                        if (dia != null)
                        {
                            errores.Add(new ValidationResult("El día que intentas guardar ya existe.", new[] { "Dia" }));
                        }
                    }
                    else
                    {
                        errores.Add(new ValidationResult("Para actualizar el día, se requiere la jornada laboral.",
                            new[] { "JornadaLaboralId" }));
                    }
                }
                #endregion

                #region  JornadaLaboralId
                if (JornadaLaboralId != null)
                {
                    if (Dia != null)
                    {
                        var dia = contexto.JornadaLaboralDias.FirstOrDefault(x => x.Id != Id &&
                                                                                  x.Dia == Dia &&
                                                                                  x.JornadaLaboralId == JornadaLaboralId);
                        if (dia != null)
                        {
                            errores.Add(new ValidationResult("El día que intentas guardar ya existe.", new[] { "Dia" }));
                        }
                    }
                    else
                    {
                        errores.Add(new ValidationResult("Para actualizar la jornada laboral, se requiere el día. ",
                            new[] { "JornadaLaboralId" }));
                    }
                }
                #endregion

                #region HoraInicioDescanso
                if (HoraInicioDescanso != null)
                {
                    if (HoraFinDescanso == null)
                        errores.Add(new ValidationResult("Si ingresaste la hora de inicio de descanso, debes ingresar la hora de fin de descanso.",
                            new[] { "HoraInicioDescanso" }));


                    if (HoraInicioJornada != null && HoraInicioJornada >= HoraInicioDescanso)
                        errores.Add(new ValidationResult("La hora que ingresaste no debe ser menor a la hora anterior.",
                            new[] { "HoraInicioDescanso" }));
                }
                #endregion

                #region HoraFinDescanso
                if (HoraFinDescanso != null)
                {
                    if (HoraInicioDescanso == null)
                        errores.Add(new ValidationResult("Si ingresaste la hora de fin de descanso, debes ingresar la hora de inicio de descanso.",
                            new[] { "HoraFinDescanso" }));


                    if (HoraInicioDescanso != null && HoraInicioDescanso >= HoraFinDescanso)
                        errores.Add(new ValidationResult("La hora que ingresaste no debe ser menor a la hora anterior.",
                            new[] { "HoraFinDescanso" }));
                }
                #endregion

                #region HoraFinJornada
                if (HoraFinJornada != null && HoraInicioDescanso != null)
                {
                    if (HoraInicioDescanso >= HoraFinJornada)
                        errores.Add(new ValidationResult("La hora que ingresaste no debe ser menor a la hora anterior.",
                            new[] { "HoraFinJornada" }));
                }
                #endregion

                #region HoraInicioJornada
                if (HoraInicioJornada != null && HoraFinJornada != null)
                {
                    if (HoraInicioJornada >= HoraFinJornada)
                        errores.Add(new ValidationResult("La hora fin jornada que ingresaste no debe ser menor a la hora inicio de jornada.",
                            new[] { "HoraInicioJornada" }));
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
