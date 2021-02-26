using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Reportes.Comandos.ConsolidadoConceptosNomina
{
    public class ReporteConsolidadoConceptosNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicial { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaFinal { get; set; }

        public string ConceptoId { get; set; }

        #endregion
        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var FechaActual = DateTime.Now;
                var fechaI = (DateTime)FechaInicial;
                var fechaF = (DateTime)FechaFinal;
                if (fechaI.Date == FechaActual.Date)
                {
                    errores.Add(new ValidationResult("La fecha inicial no puede ser igual a la fecha actual.",
                            new[] { "FechaInicial" }));
                }
                if (fechaI.Date > FechaActual.Date)
                {
                    errores.Add(new ValidationResult("La fecha inicial no puede ser posterior a la fecha actual.",
                            new[] { "FechaInicial" }));
                }
                if (fechaI.Date == fechaF.Date)
                {
                    errores.Add(new ValidationResult("La fecha final no puede ser igual a la fecha inicial.",
                            new[] { "FechaFinal" }));
                }
                if (fechaF.Date > FechaActual.Date)
                {
                    errores.Add(new ValidationResult("La fecha final no puede ser posterior a la fecha actual.",
                            new[] { "FechaFinal" }));
                }
                if (fechaF.Date < fechaI.Date)
                {
                    errores.Add(new ValidationResult("La fecha final no puede ser menor a la fecha inicial.",
                                new[] { "FechaFinal" }));
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
