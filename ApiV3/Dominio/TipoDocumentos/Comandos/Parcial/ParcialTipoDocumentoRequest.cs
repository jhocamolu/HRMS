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
/// Clase Encargada de realizar las validaciones para  actualizaciones 
/// parciales a  la entidad TipoDocumento
/// </summary>

namespace ApiV3.Dominio.TipoDocumentos.Comandos.Estado
{
    public class ParcialTipoDocumentoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(2, ErrorMessage = ConstantesErrores.Maximo + "2.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string CodigoPila { get; set; }

        [MaxLength(2, ErrorMessage = ConstantesErrores.Maximo + "2.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string CodigoDian { get; set; }

        [MaxLength(150, ErrorMessage = ConstantesErrores.Maximo + "150.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [EnumDataType(typeof(FormatoValidacion), ErrorMessage = ConstantesErrores.FormatoDelEmunerador)]
        public FormatoValidacion? Formato { get; set; }

        public string EquivalenteBancario { get; set; }
        #endregion

        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Id
                //Valida_Si_Existe_ID
                var idTipoDocumento = contexto.TipoDocumentos.FirstOrDefault(x => x.Id == Id);
                if (idTipoDocumento == null)
                {
                    errores.Add(new ValidationResult("No existe Id.", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region CodigoDian 
                //Si existe CodigoDian
                var codigoDian = contexto.TipoDocumentos.FirstOrDefault(x => x.Id != Id && x.CodigoDian == CodigoDian);
                if (codigoDian != null)
                {
                    errores.Add(new ValidationResult(
                       "El código DIAN que intentas guardar ya existe.",
                       new[] { "CodigoDian" }));
                }
                #endregion

                #region CodigoPila
                var codigoPila = contexto.TipoDocumentos.FirstOrDefault(x => x.Id != Id && x.CodigoPila == CodigoPila);
                //Si existe CodigoPila
                if (codigoPila != null)
                {
                    errores.Add(new ValidationResult(
                       "El código pila que intentas guardar ya existe.",
                       new[] { "CodigoPila" }));
                }
                #endregion

                #region EquivalenteBancario
                if (EquivalenteBancario != null)
                {
                    var equivalenteBancario = contexto.TipoDocumentos.FirstOrDefault(x => x.EquivalenteBancario == EquivalenteBancario);
                    if (equivalenteBancario != null)
                    {
                        errores.Add(new ValidationResult(
                           "El equivalente bancario que intentas guardar ya existe.",
                           new[] { "EquivalenteBancario" }));
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
