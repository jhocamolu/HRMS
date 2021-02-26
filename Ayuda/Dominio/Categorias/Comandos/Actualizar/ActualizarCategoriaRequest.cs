using Ayuda.Dominio.Utilidades;
using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Actualizar
{
    public class ActualizarCategoriaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

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

                #region id
                var element = dbContext.Categorias.FirstOrDefault(x => x.Id == Id);
                if (element == null)
                {
                    errors.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                    return errors;
                }
                #endregion

                //#region Activo
                //if (Activo == false)
                //{
                //    var elemento = dbContext.Categorias.FirstOrDefault(x => x.CategoriaId == Id);
                //    if (elemento != null)
                //    {
                //        errors.Add(new ValidationResult(
                //         $"La categoria {Nombre} contiene subcategorias {Id} no se puede desactivar",
                //         new[] { "Id" }));
                //    }
                //}
                //#endregion



                #region Orden
                if (Orden > 0)
                {
                    var elemento = dbContext.Categorias.FirstOrDefault(x =>
                                                                        (x.CategoriaId == CategoriaId)
                                                                        && (x.Orden == Orden)
                                                                        && (x.Id != Id)
                                                                       );
                    if (elemento != null)
                    {
                        errors.Add(new ValidationResult(
                         $"Ya existe una categoría hermana con el mismo orden.",
                         new[] { "Orden" }));
                    }
                }
                #endregion

                #region CategoriaId
                if (CategoriaId != null)
                {

                    // Permite buscar si una categoria existe
                    var elemento = dbContext.Categorias.FirstOrDefault(x => x.Id == CategoriaId && (x.Id != Id));
                    if (elemento == null)
                    {
                        errors.Add(new ValidationResult(
                           $"La categoría padre que intentas guardar no existe",
                           new[] { "CategoriaPadreId" }));
                    }

                    // Esto permite buscar si una categoria padre tiene hijos
                    var elementoPadre = dbContext.Categorias.FirstOrDefault(x => x.CategoriaId == Id);
                    if (elementoPadre != null)
                    {
                        errors.Add(new ValidationResult(
                          $"Está categoría no puede pertenecer a la categoría padre seleccionada.",
                          new[] { "CategoriaPadreId" }));
                        return errors;
                    }

                    // Valida si una categoria es Nivel 1
                    if (elemento.CategoriaId != null)
                    {
                        errors.Add(new ValidationResult(
                          $"La categoría padre que intentas guardar no es nivel 1.",
                          new[] { "CategoriaPadreId" }));
                    }
                    return errors;
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
