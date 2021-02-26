using Ayuda.Dominio.Utilidades;
using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Parcial
{
    public class ParcialCategoriaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        public bool Activo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                var dbContext = (AyudaDbContext)validationContext.GetService(typeof(AyudaDbContext));
                #region id
                var element = dbContext.Categorias.FirstOrDefault(x => x.Id == Id);
                if (element == null)
                {
                    errors.Add(new ValidationResult(
                       $"No Existe.",
                       new[] { "Id" }));
                    return errors;
                }
                #endregion
                if (Activo == false)
                {
                    if (element.CategoriaId == null)
                    {
                        #region validarhijosyarticulos
                        var subcategorias = dbContext.Categorias.FirstOrDefault(x => x.CategoriaId == Id
                                                                                      && x.EstadoRegistro.ToString().Contains("Activo")
                                                                                );
                        if (subcategorias != null)
                        {
                            errors.Add(new ValidationResult(
                               $"No se puede inactivar. Debe primero inactivar subcategorías de la categoría.",
                               new[] { "Id" }));
                            return errors;
                        }

                        var articulos = dbContext.Articulos.FirstOrDefault(x => x.CategoriaId == Id
                                                                                     && x.EstadoRegistro.ToString().Contains("Activo")
                                                                               );
                        if (articulos != null)
                        {
                            errors.Add(new ValidationResult(
                               $"No se puede inactivar. Debe primero inactivar artículos de la categoría.",
                               new[] { "Id" }));
                            return errors;
                        }

                        #endregion
                    }
                    else
                    {
                        #region ValidaArticulos
                        var articulos = dbContext.Articulos.FirstOrDefault(x => x.CategoriaId == Id
                                                                                     && x.EstadoRegistro.ToString().Contains("Activo")
                                                                               );
                        if (articulos != null)
                        {
                            errors.Add(new ValidationResult(
                               $"Existen artículos de la categoría.",
                               new[] { "Id" }));
                            return errors;
                        }
                        #endregion
                    }
                }
               
            }
            catch (Exception e)
            {
                errors.Add(new ValidationResult(e.Message));
            }

            return errors;
        }
    }
}
