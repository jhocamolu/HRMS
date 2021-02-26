using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Actualizar
{
    public class ActualizarHojaDeVidaEstudioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? HojaDeVidaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? NivelEducativoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        public string InstitucionEducativa { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? PaisId { get; set; }

        public int? ProfesionId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(EstadoEstudio), ErrorMessage = "No es un estado valido.")]
        public EstadoEstudio? EstadoEstudio { get; set; }

        [MaxLength(20, ErrorMessage = ConstantesErrores.Maximo + " 20.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.Guion + "]+$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string TarjetaProfesional { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + "]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Titulo { get; set; }

        public string Observacion { get; set; }
        #endregion

        #region Validacion Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            try
            {
                #region Id
                var existe = dbContexto.HojaDeVidaEstudios.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {

                    errores.Add(new ValidationResult(
                        $"No existe este estudio.",
                        new[] { "Id" }));
                }
                #endregion

                #region HojaDeVidaId
                var existeHojaDeVida = dbContexto.HojaDeVidas.FirstOrDefault(x => x.Id == HojaDeVidaId);
                if (existeHojaDeVida == null)
                {

                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("hoja de vida"),
                        new[] { "HojaDeVidaId" })); ;
                }
                #endregion

                #region NivelEducativoId
                var existeNivelEducativo = dbContexto.NivelEducativos.FirstOrDefault(x => x.Id == NivelEducativoId);
                if (existeNivelEducativo == null)
                {

                    errores.Add(new ValidationResult($"No existe este nivel educativo.",
                        new[] { "NivelEducativoId" }));
                }
                #endregion

                #region PaisId
                var existePais = dbContexto.Paises.FirstOrDefault(x => x.Id == PaisId);
                if (existePais == null)
                {

                    errores.Add(new ValidationResult(
                        $"No existe este pais.",
                        new[] { "PaisId" }));
                }
                #endregion

                #region ProfesionId
                if (ProfesionId != null)
                {
                    var existeProfesion = dbContexto.Profesiones.FirstOrDefault(x => x.Id == ProfesionId);
                    if (existeProfesion == null)
                    {

                        errores.Add(new ValidationResult(
                            $"No existe esta profesion.",
                            new[] { "ProfesionId" }));
                    }

                }
                #endregion

                #region CompararFechas
                if (FechaInicio != null && FechaInicio < DateTime.Parse("1974-01-01"))
                {
                    errores.Add(new ValidationResult(
                            $"El fecha de inicio no puede ser menor a 1954-01-01.",
                            new[] { "FechaInicio" }));
                }
                if (FechaInicio != null && FechaFin != null && FechaInicio > FechaFin)
                {
                    errores.Add(new ValidationResult(
                            $"La fecha de inicio no puede ser mayor a la fecha de finalización.",
                            new[] { "FechaInicio" }));
                }

                if (FechaInicio != null && FechaFin != null && FechaFin < FechaInicio)
                {

                    errores.Add(new ValidationResult(
                        $"La fecha de finalización no puede ser menor a la fecha de inicio.",
                        new[] { "FechaFin" }));

                }
                #endregion

                #region estado FechaFin
                if (EstadoEstudio == Infraestructura.Enumerador.EstadoEstudio.Culminado)
                {
                    if (FechaFin == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "FechaFin" }));
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
