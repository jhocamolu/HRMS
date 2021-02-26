using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Crear
{
    public class CrearHojaDeVidaExperienciaLaboralRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int HojaDeVidaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string NombreCargo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[ " + ConstantesExpresionesRegulares.Numerico + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string NombreEmpresa { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(7, ErrorMessage = ConstantesErrores.Minimo + " 7.")]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        [RegularExpression(@"^[ " + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Telefono { get; set; }

        [Range(0, 9999999999, ErrorMessage = ConstantesErrores.Rango + "0 - 9999999999.")]
        [RegularExpression(@"^[ " + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Salario { get; set; }

        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string NombreJefeInmediato { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public string FuncionesCargo { get; set; }

        public bool TrabajaActualmente { get; set; }

        public string MotivoRetiro { get; set; }

        public string Observaciones { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region HojaDeVida 
                var existeHojaDeVida = contexto.HojaDeVidas.FirstOrDefault(x => x.Id == HojaDeVidaId);
                if (existeHojaDeVida == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "HojaDeVidaId" }));
                }
                #endregion

                #region FechaInicio
                DateTime fechaVacia = DateTime.MinValue;
                if (FechaInicio == fechaVacia)
                {
                    errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaInicio" }));
                }
                else
                {
                    DateTime fechaActual = DateTime.Today;
                    if (FechaInicio > fechaActual)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.FechaNoMayorActual,
                            new[] { "FechaInicio" }));
                    }
                }
                if (FechaFin != fechaVacia)
                {
                    if (FechaFin < FechaInicio)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.FechaFinNoMayorFechaInicial,
                            new[] { "FechaFin" }));
                    }
                    if (FechaInicio > FechaFin)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.FechaInicialNoMayorFechaFin,
                            new[] { "FechaInicio" }));
                    }
                }
                #endregion

                #region TrabajaActualmente
                if (TrabajaActualmente == false && FechaFin == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                            new[] { "FechaFin" }));
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