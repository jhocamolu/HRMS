using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.JornadaLaboralDias.Comandos.Actualizar
{
    public class ActualizarJornadaLaboralDiaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int JornadaLaboralId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(DiaSemana), ErrorMessage = ConstantesErrores.FormatoDelEmunerador)]
        public DiaSemana Dia { get; set; }

        #region HorarioLaboral
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public TimeSpan HoraInicioJornada { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public TimeSpan HoraFinJornada { get; set; }
        #endregion

        #region HorarioDescanso
        public TimeSpan? HoraInicioDescanso { get; set; }



        //[Column(TypeName = "time(7)")]
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
                    errores.Add(new ValidationResult($"No existe Id.", new[] { "Id" }));
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
                var dia = contexto.JornadaLaboralDias.FirstOrDefault(x => x.Id != Id &&
                                                                            x.Dia == Dia &&
                                                                            x.JornadaLaboralId == JornadaLaboralId);
                if (dia != null)
                    errores.Add(new ValidationResult("El día que intentas guardar ya existe.",
                        new[] { "Dia" }));
                #endregion

                #region HoraInicioDescanso
                if (HoraFinDescanso != null && HoraInicioDescanso == null)
                    errores.Add(new ValidationResult("Si ingresaste la hora de fin de descanso, debes ingresar la hora de inicio de descanso.",
                        new[] { "HoraInicioDescanso" }));


                if (HoraInicioDescanso >= HoraFinDescanso)
                    errores.Add(new ValidationResult("La hora que ingresaste no debe ser menor a la hora anterior.",
                        new[] { "HoraInicioDescanso" }));
                #endregion

                #region HoraFinDescanso
                if (HoraInicioDescanso != null && HoraFinDescanso == null)
                    errores.Add(new ValidationResult("Si ingresaste la hora de inicio de descanso, debes ingresar la hora de fin de descanso.",
                        new[] { "HoraFinDescanso" }));

                if (HoraInicioJornada >= HoraInicioDescanso)
                    errores.Add(new ValidationResult("La hora que ingresaste no debe ser menor a la hora anterior.",
                        new[] { "HoraFinDescanso" }));



                #endregion

                #region HoraFinJornada
                if (HoraInicioDescanso >= HoraFinJornada)
                    errores.Add(new ValidationResult("La hora que ingresaste no debe ser menor a la hora anterior.",
                        new[] { "HoraFinJornada" }));
                #endregion

                #region HoraInicioJornada
                if (HoraInicioJornada >= HoraFinJornada)
                    errores.Add(new ValidationResult("La hora fin jornada que ingresaste no debe ser menor a la hora inicio de jornada.",
                        new[] { "HoraInicioJornada" }));
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
