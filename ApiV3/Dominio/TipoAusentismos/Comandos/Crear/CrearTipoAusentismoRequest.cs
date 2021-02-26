using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoAusentismos.Comandos.Crear
{
    public class CrearTipoAusentismoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                 ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ClaseAusentismoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(UnidadTiempo), ErrorMessage = "No es una unidad de tiempo valida.")]
        public UnidadTiempo? UnidadTiempo { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region ConceptoNomina
                var validaUnico = contexto.TipoAusentismos.FirstOrDefault(x => x.Nombre == Nombre);
                if (validaUnico != null)
                {
                    errores.Add(new ValidationResult(
                       $"El nombre del tipo de ausentismo que intentas guardar ya existe.",
                       new[] { "Nombre" }));
                }
                #endregion

                #region ClaseAusentismo
                //Valida que exista Clase Ausentismo
                var validaClaseAusentismo = contexto.ClaseAusentismos.FirstOrDefault(x => x.Id == ClaseAusentismoId);
                if (validaClaseAusentismo == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "ClaseAusentismoId" }));
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
