using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Calendarios.Comandos.Crear
{
    public class CrearCalendarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Nombre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var existefecha = dbContexto.Calendarios.FirstOrDefault(x => x.Fecha == Fecha);
                if (existefecha != null)
                {
                    errores.Add(new ValidationResult(
                       $"La fecha del festivo que intentas guardar ya existe.",
                       new[] { "Fecha" }));
                }
                if (Fecha.Year < DateTime.Today.Year - 2)
                {
                    errores.Add(new ValidationResult(
                        $"No se permiten guardar fechas de m�s de dos a�os atr�s.",
                        new[] { "Fecha" }));
                }

                if (Fecha.Year > DateTime.Today.Year + 2)
                {
                    errores.Add(new ValidationResult(
                        $"No se permiten guardar fechas de m�s de dos a�os hacia delante.",
                        new[] { "Fecha" }));
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