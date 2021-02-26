using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NivelCargos.Comandos.Crear
{
    public class CrearNivelCargoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1")]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico + " ]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]

        public string Nombre { get; set; }
        #endregion

        #region Validaciones Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                //Valida que el nombre sea único
                var elemento = dbContexto.NivelCargos.FirstOrDefault(x => x.Nombre == Nombre);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                       $"El nivel de cargo que intentas guardar ya existe.",
                       new[] { "Nombre" }));
                }
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
