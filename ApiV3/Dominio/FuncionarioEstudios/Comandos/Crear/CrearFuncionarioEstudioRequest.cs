using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.FuncionarioEstudios.Comandos.Crear
{
    public class CrearFuncionarioEstudioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

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
        public DateTime? AnioDeInicio { get; set; }

        public DateTime? AnioDeFin { get; set; }

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

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            try
            {
                #region FuncionarioId
                var existeFuncionario = dbContexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                if (existeFuncionario == null)
                {

                    errores.Add(new ValidationResult(
                        $"No existe este funcionario.",
                        new[] { "FuncionarioId" }));
                }
                #endregion

                #region NivelEducativoId
                var existeNivelEducativo = dbContexto.NivelEducativos.FirstOrDefault(x => x.Id == NivelEducativoId);
                if (existeNivelEducativo == null)
                {

                    errores.Add(new ValidationResult(
                        $"No existe este nivel educativo.",
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

                if (AnioDeInicio < DateTime.Parse("1974-01-01"))
                {
                    errores.Add(new ValidationResult(
                            $"El año de inicio no puede ser menor a 1954-01-01.",
                            new[] { "AnioDeInicio" }));
                }

                if (AnioDeFin != null)
                {
                    if (AnioDeInicio > AnioDeFin)
                    {
                        errores.Add(new ValidationResult(
                            $"El año de inicio no puede ser mayor al año de finalización.",
                            new[] { "AnioDeInicio" }));
                    }
                }
                #endregion

                #region estado FechaFin
                if (EstadoEstudio == Infraestructura.Enumerador.EstadoEstudio.Culminado)
                {
                    if (AnioDeFin == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "AnioDeFin" }));
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
