using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Reportes.Comandos.LibroVacaciones
{
    public class ReporteLibroVacacionesRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion

        public string Periodo { get; set; }

        public int? FuncionarioId { get; set; }

        public string EstadoContrato { get; set; }

        public string CentroOperativoId { get; set; }

        public string DependenciaId { get; set; }

        #endregion
        #region ValidacionManual

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                if (Periodo == null)
                {
                    errores.Add(new ValidationResult("Requerido",
                                        new[] { "Periodo" }));
                }

                if (FuncionarioId == null && EstadoContrato == null)
                {
                    errores.Add(new ValidationResult("Requerido",
                                        new[] { "EstadoContrato" }));
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
