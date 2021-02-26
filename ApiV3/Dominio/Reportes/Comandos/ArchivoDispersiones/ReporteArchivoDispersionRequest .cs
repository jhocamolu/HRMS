using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Reportes.Comandos.ArchivoDispersiones
{
    public class ReporteArchivoDispersionRequest : IRequest<CommandResult>, IValidatableObject
    {

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public List<int> Liquidaciones { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CuentaBancariaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaCuentaBancaria = contexto.CuentaBancarias.FirstOrDefault(f => f.Id == CuentaBancariaId);
                if (validaCuentaBancaria == null)
                {
                    errores.Add(new ValidationResult(
                           $"{ConstantesErrores.NoExiste("la cuenta bancaria")}",
                           new[] { "CuentaBancariaId" }));
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
