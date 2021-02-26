using Ayuda.Dominio.Utilidades;
using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Comandos.Actualizar
{
    public class ActualizarArticuloRequest: IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(3, ErrorMessage = ConstantesErrores.Minimo + " 3.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 9999, ErrorMessage = ConstantesErrores.Rango + "1 - 9999.")]
        public int? Orden { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public List<string> Palabras { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            try
            {
                var dbContext = (AyudaDbContext)validationContext.GetService(typeof(AyudaDbContext));
                #region Orden
                if (Orden > 0)
                {
                    var element = dbContext.Articulos.FirstOrDefault(x =>
                                                                      (x.CategoriaId == CategoriaId)
                                                                      && x.Orden == Orden
                                                                      && x.Id != Id
                                                                     );
                    if (element != null)
                    {
                        errors.Add(new ValidationResult(
                         $"Ya existe un artículo de ayuda hermano con el mismo orden.",
                         new[] { "Orden" }));
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                return errors;
            }
            return errors;
        }
    }
}
