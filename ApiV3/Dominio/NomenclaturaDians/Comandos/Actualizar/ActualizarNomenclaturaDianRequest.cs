using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NomenclaturaDians.Comandos.Actualizar
{
    public class ActualizarNomenclaturaDianRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + " 15")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Numeral + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.Guion + "]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + " 40.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + "]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool TextoPosterior { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                var dbContext = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id

                // Elemento no existe
                var existe = dbContext.NomenclaturaDians.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errors.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));

                    return errors;
                }
                #endregion

                #region Codigo

                //Valida que código sea único
                var element = dbContext.NomenclaturaDians.FirstOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                if (element != null)
                {
                    errors.Add(new ValidationResult(
                       $"Ya existe una nomenclatura con el código ingresado",
                       new[] { "Codigo" }));
                }
                #endregion
            }
            catch (Exception e)
            {
                errors.Add(new ValidationResult(e.Message));
            }

            return errors;
        }
    }
}
