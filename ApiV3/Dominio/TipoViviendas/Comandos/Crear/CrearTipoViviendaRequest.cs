using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoViviendas.Comandos.Crear
{
    public class CrearTipoViviendaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(30, ErrorMessage = ConstantesErrores.Maximo + " 30.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                //Valida que Nombre sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var elemento = dbContexto.TipoViviendas.SingleOrDefault(x => x.Nombre == Nombre);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El tipo de vivienda que intentas guardar ya existe.",
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
