using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NominaDetalle.Comandos.Actualizar
{
    public class ActualizarNominaDetalleRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Range(minimum: 0, maximum: 100000, ErrorMessage = ConstantesErrores.Rango + "0 - 100000.")]
        public double? Cantidad { get; set; }

        [Range(minimum: 0, maximum: 100000000, ErrorMessage = ConstantesErrores.Rango + "0 - 100000000.")]
        public double? Valor { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Observacion { get; set; }
        #endregion

        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaNomina = dbContexto.NominaDetalles.FirstOrDefault(x => x.Id == Id);
                if (validaNomina == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("nómina detalle"),
                       new[] { "Id" }));
                }

                var validaConceptoNomina = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == validaNomina.ConceptoNominaId);
                if (validaConceptoNomina.RequiereCantidad == true)
                {
                    if (Cantidad == null)
                    {
                        errores.Add(new ValidationResult("Requerido",
                        new[] { "Cantidad" }));
                    }

                }
                else
                {
                    if (Valor == null)
                    {
                        errores.Add(new ValidationResult("Requerido",
                       new[] { "Valor" }));
                    }
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
