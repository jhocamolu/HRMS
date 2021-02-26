using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.AplicacionExternas.Comandos.Actualizar
{
    public class ActualizarAplicacionExternaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [EnumDataType(typeof(TipoAplicacionExterna), ErrorMessage = "No es un estado valido.")]
        public TipoAplicacionExterna? Aprueba { get; set; }

        [Required]
        [EnumDataType(typeof(TipoAplicacionExterna), ErrorMessage = "No es un estado valido.")]
        public TipoAplicacionExterna? Autoriza { get; set; }

        [Required]
        public TipoAplicacionExterna? Revisa { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Nombre
                //Valida que nombre sea único
                var validaNombre = dbContexto.AplicacionExternas.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (validaNombre != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre que intentas guardar ya existe.",
                        new[] { "Nombre" }));
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
