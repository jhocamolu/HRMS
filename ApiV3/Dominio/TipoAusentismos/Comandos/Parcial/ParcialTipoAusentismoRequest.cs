using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoAusentismos.Comandos.Parcial
{
    public class ParcialTipoAusentismoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        public string Nombre { get; set; }

        public int? ClaseAusentismoId { get; set; }

        [EnumDataType(typeof(UnidadTiempo), ErrorMessage = "No es una unidad de tiempo valida.")]
        public UnidadTiempo? UnidadTiempo { get; set; }

        public bool? Activo { get; set; }
        #endregion
        #region Validaciones Manuales 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Nombre
                if (Nombre != null)
                {
                    var validaUnico = contexto.TipoAusentismos.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                    if (validaUnico != null)
                    {
                        errores.Add(new ValidationResult(
                           $"El nombre del tipo de ausentismo que intentas guardar ya existe.",
                           new[] { "Nombre" }));
                    }
                }

                #endregion

                #region ClaseAusentismo
                if (ClaseAusentismoId != null)
                {
                    //Valida que exista Clase Ausentismo
                    var validaClaseAusentismo = contexto.ClaseAusentismos.FirstOrDefault(x => x.Id == ClaseAusentismoId);
                    if (validaClaseAusentismo == null)
                    {
                        errores.Add(new ValidationResult(
                           $"No Existe",
                           new[] { "ClaseAusentismoId" }));
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
