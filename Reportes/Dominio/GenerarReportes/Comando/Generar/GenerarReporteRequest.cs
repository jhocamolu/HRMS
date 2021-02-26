using MediatR;
using Microsoft.EntityFrameworkCore;
using Reportes.Infraestructura.DbContexto;
using Reportes.Infraestructura.Resultados;
using Reportes.Infraestructura.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reportes.Dominio.GenerarReportes.Comando.Generar
{
    public class GenerarReporteRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ReporteId { get; set; }

        public Dictionary<string, string> Parametros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var contexto = (ReportesDbContext)validationContext.GetService(typeof(ReportesDbContext));
                #region ReporteId
                //Valida exista Reporte
                var reporte = contexto.Reportes.FirstOrDefault(x => x.Id == ReporteId);
                if (reporte == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe este reporte",
                        new[] { "ReporteId" }));
                }
                #endregion

                #region Parametros
                //Valida 
                var reporteParametros = contexto.ReporteParametros.Include(x => x.Parametro).Where(x => x.ReporteId == ReporteId).ToList();

                if (reporteParametros.Any() && Parametros.Any())
                {
                    foreach (var item in Parametros)
                    {
                        var parametro = reporteParametros.FirstOrDefault(x => x.Parametro.Nombre == item.Key);
                        if (parametro == null)
                        {
                            errores.Add(new ValidationResult(
                                        $"No existe este parametro",
                                        new[] { item.Key }));
                        }
                        if (parametro.EsRequerido)
                        {
                            if (item.Value == null)
                            {
                                errores.Add(new ValidationResult(
                                            $"{ConstantesErrores.Requerido}",
                                            new[] { item.Key }));
                            }
                        }

                    }
                }
                else if (reporteParametros.Any() && !Parametros.Any())
                {
                    errores.Add(new ValidationResult(
                                        $"Este reporte tiene parametros asignados.",
                                        new[] { "ReporteId" }));
                }
                #endregion
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }

            return errores;
        }
    }
}
