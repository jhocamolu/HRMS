using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CategoriaNovedades.Comandos.Parcial
{
    public class ParcialCategoriaNovedadRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                var elemento = dbContexto.CategoriaNovedades.FirstOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe.",
                       new[] { "Id" }));

                    return errores;
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
