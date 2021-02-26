using Ayuda.Dominio.Utilidades;
using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Crear
{
    public class CrearCategoriaRequest : IRequest<CommandResult>, IValidatableObject
    {

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(3, ErrorMessage = ConstantesErrores.Minimo + " 3.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 9999, ErrorMessage = ConstantesErrores.Rango + "1 - 9999.")]
        public int? Orden { get; set; }

        public int? CategoriaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                var dbContext = (AyudaDbContext)validationContext.GetService(typeof(AyudaDbContext));
                var element = dbContext.Categorias.FirstOrDefault(x =>
                                                                        (x.CategoriaId == CategoriaId)
                                                                        && x.Orden == Orden
                                                                       );
                #region Orden

                if (element != null && Orden == element.Orden || Orden <= 0)
                {

                    errors.Add(new ValidationResult(
                        $"Ya existe una categoría hermana con el mismo orden.",
                        new[] { "Orden" }));

                }
                #endregion
                #region CategoriaId
                if (CategoriaId != null)
                {

                    element = dbContext.Categorias.FirstOrDefault(x => x.Id == CategoriaId);
                    if (element == null)
                    {
                        errors.Add(new ValidationResult(
                           $"El codigo {CategoriaId} no existe",
                           new[] { "CategoriaPadreId" }));
                    }
                    else if (element.CategoriaId != null)
                    {
                        errors.Add(new ValidationResult(
                           $"La categoría padre que intentas guardar no es nivel 1.",
                          new[] { "CategoriaPadreId" }));
                    }
                }
                return errors;
                #endregion
            }
            catch (Exception)
            {

            }
            return errors;
        }
    }
}
