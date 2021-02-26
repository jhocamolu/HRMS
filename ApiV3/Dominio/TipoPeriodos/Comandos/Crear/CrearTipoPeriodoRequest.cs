using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoPeriodos.Comandos.Crear
{
    public class CrearTipoPeriodoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                 ConstantesExpresionesRegulares.Espacio + " ]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? PagoPorDefecto { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Nombre
                var validaUnico = contexto.TipoPeriodos.FirstOrDefault(x => x.Nombre == Nombre);
                if (validaUnico != null)
                {
                    errores.Add(new ValidationResult(
                       $"El tipo de período que intentas guardar ya existe.",
                       new[] { "Nombre" }));
                }
                #endregion
                #region PagoPorDefecto
                if (PagoPorDefecto == true)
                {
                    var existePagoPorDefecto = contexto.TipoPeriodos.FirstOrDefault(x => x.PagoPorDefecto == true);
                    if (existePagoPorDefecto != null)
                    {
                        errores.Add(new ValidationResult("Ya existe un tipo de período por defecto.",
                            new[] { "PagoPorDefecto" }));
                    }
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
