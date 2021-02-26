using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Reportes.Comandos.ArchivoTipo1Pilas
{
    public class ReporteArchivoTipo1PilaRequest : IRequest<CommandResult>, IValidatableObject
    {

        public int? TipoAccionId { get; set; }

        public DateTime? FechaInicial { get; set; }

        public DateTime? FechaFinal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                if (TipoAccionId != null)
                {
                    var validaTipoAccion = contexto.TipoAcciones.FirstOrDefault(x => x.Id == TipoAccionId);
                    if (validaTipoAccion == null)
                    {
                        errores.Add(new ValidationResult(
                                $"{ConstantesErrores.NoExiste("el tipo de acción")}",
                                new[] { "TipoAccionId" }));
                    }

                    if (FechaInicial == null)
                    {
                        errores.Add(new ValidationResult(
                                $"{ConstantesErrores.Requerido}",
                                new[] { "FechaInicial" }));
                    }
                    if (FechaFinal == null)
                    {
                        errores.Add(new ValidationResult(
                                $"{ConstantesErrores.Requerido}",
                                new[] { "FechaFinal" }));
                    }

                    if (FechaInicial > FechaFinal)
                    {
                        errores.Add(new ValidationResult(
                               $"La fecha inicial no debe ser mayor a la fecha final.",
                               new[] { "FechaInicial" }));
                    }
                    if (FechaFinal < FechaInicial)
                    {
                        errores.Add(new ValidationResult(
                               $"La fecha final no debe ser menor a la fecha inicial.",
                               new[] { "FechaFinal" }));
                    }
                }


            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
    }
}
