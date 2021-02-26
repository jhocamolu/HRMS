using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoContratos.Comandos.Actualizar
{
    public class ActualizarTipoContratoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion 
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + "60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(ClaseTipoContrato))]
        public ClaseTipoContrato? Clase { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(0, 10, ErrorMessage = ConstantesErrores.Rango + "0 - 10.")]
        public string CantidadProrrogas { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(0, 99999, ErrorMessage = ConstantesErrores.Rango + "0 - 99999.")]
        public string DuracionMaxima { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? TerminoIndefinido { get; set; }

        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string DocumentoSlug { get; set; }
        #endregion
        #region ValidacionesManuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            try
            {
                //Valida que registro sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var elemento = dbContexto.TipoContratos.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);

                if (elemento != null)
                {
                    errors.Add(new ValidationResult(
                        $"El tipo de contrato que intentas guardar ya existe.",
                        new[] { "Nombre" }));
                }
            }
            catch (Exception e)
            {
                errors.Add(new ValidationResult(e.Message));
            }
            return errors;
        }
        #endregion
    }
}
