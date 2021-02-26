using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NivelCargos.Comandos.Parcial
{
    public class ParcialNivelCargoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1")]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico + " ]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }

        public bool? Activo { get; set; }
        #endregion

        #region Validaciones Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                //Valida si elemento Existe
                var existe = dbContexto.NivelCargos.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }

                //Valida que el nombre sea único
                if (Nombre != null)
                {
                    var elemento = dbContexto.NivelCargos.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                    if (elemento != null)
                    {
                        errores.Add(new ValidationResult(
                           $"El nivel de cargo que intentas guardar ya existe.",
                           new[] { "Nombre" }));
                    }
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
