using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de la validacion de los campos para Crear. 
/// Tener en cuenta que el campo formato solo recive Alfanumerico O
/// Numerico que se validan con el Emun NombreDocumentoFormato
/// </summary>

namespace ApiV3.Dominio.TipoDocumentos.Comandos.Crear
{
    public class CrearTipoDocumentoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(2, ErrorMessage = ConstantesErrores.Maximo + "2.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string CodigoPila { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(2, ErrorMessage = ConstantesErrores.Maximo + "2.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string CodigoDian { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(150, ErrorMessage = ConstantesErrores.Maximo + "150.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                 ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(FormatoValidacion), ErrorMessage = ConstantesErrores.FormatoDelEmunerador)]
        public FormatoValidacion Formato { get; set; }


        public string EquivalenteBancario { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                //Buscamos si hay coninsidencias en la base de datos
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region CodigoDian
                //Si existe CodigoDian
                var codigoDian = contexto.TipoDocumentos.FirstOrDefault(x => x.CodigoDian == CodigoDian);
                if (codigoDian != null)
                {
                    errores.Add(new ValidationResult(
                       "El código DIAN que intentas guardar ya existe.",
                       new[] { "CodigoDian" }));
                }
                #endregion

                #region CodigoPila
                //Si existe CodigoPila
                var codigoPila = contexto.TipoDocumentos.FirstOrDefault(x => x.CodigoPila == CodigoPila);
                if (codigoPila != null)
                {
                    errores.Add(new ValidationResult(
                       "El código pila que intentas guardar ya existe.",
                       new[] { "CodigoPila" }));
                }
                #endregion

                #region EquivalenteBancario
                //Si existe CodigoPila
                var equivalenteBancario = contexto.TipoDocumentos.FirstOrDefault(x => x.EquivalenteBancario == EquivalenteBancario);
                if (equivalenteBancario != null)
                {
                    errores.Add(new ValidationResult(
                       "El equivalente bancario que intentas guardar ya existe.",
                       new[] { "EquivalenteBancario" }));
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
