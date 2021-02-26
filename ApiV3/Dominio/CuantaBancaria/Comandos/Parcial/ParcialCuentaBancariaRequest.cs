using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CuantaBancaria.Comandos.Parcial
{
    public class ParcialCuentaBancariaRequest : IRequest<CommandResult>, IValidatableObject
    {

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? Activo { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var cuenta = dbContexto.CuentaBancarias.FirstOrDefault(x => x.Id == Id);
                if (cuenta == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cuenta bancaria"),
                       new[] { "Id" }));

                    return errores;
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
