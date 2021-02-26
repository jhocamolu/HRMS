using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Reportes.Comandos.FamiliaresFuncionario
{
    public class ReporteFamiliaresFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion

        public string TipoParentescoId { get; set; }

        public int? FuncionarioId { get; set; }

        public string CentroOperativoId { get; set; }

        public string DependenciaId { get; set; }

        public string CargoId { get; set; }

        #endregion
        #region ValidacionManual

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                if (FuncionarioId != null)
                {
                    var estadoContrato = dbContexto.Contratos.FirstOrDefault(x => x.FuncionarioId == FuncionarioId);
                    if (estadoContrato == null)
                    {
                        errores.Add(new ValidationResult("El funcionario al que intentas generarle el reporte, no se encuentra con un contrato vigente. Por favor revisa.",
                               new[] { "FuncionarioId" }));
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
