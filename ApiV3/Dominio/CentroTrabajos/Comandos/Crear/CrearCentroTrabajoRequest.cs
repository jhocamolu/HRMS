using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CentroTrabajos.Comandos.Crear
{
    public class CrearCentroTrabajoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(9, ErrorMessage = ConstantesErrores.Maximo + " 9.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + " 40.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + " ]+$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public double PorcentajeRiesgo { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                #region Codigo
                //Valida que código sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var elemento = dbContexto.CentroTrabajos.FirstOrDefault(x => x.Codigo == Codigo);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El centro de trabajo que intentas guardar ya existe.",
                        new[] { "Codigo" }));
                }
                #endregion

                #region PorcentajeRiesgo 
                /// Valida que porcentaje de riesgo cumpla con hasta 5 decimales
                if (PorcentajeRiesgo > 100)
                {
                    errores.Add(new ValidationResult(
                        $"Rango de 0 - 100",
                        new[] { "PorcentajeRiesgo" }));
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