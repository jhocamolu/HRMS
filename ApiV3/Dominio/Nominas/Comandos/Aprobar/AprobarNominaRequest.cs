using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Nominas.Comandos.Aprobar
{
    public class AprobarNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? Aprobar { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaNomina = dbContexto.Nominas.FirstOrDefault(x => x.Id == Id);
                if (Aprobar != null)
                {
                    if (Aprobar == true && validaNomina.Estado != EstadoNomina.Liquidada)
                    {
                        errores.Add(new ValidationResult("La nómina seleccionada no esta Liquidada.",
                           new[] { "SnackError" }));
                    }
                    else
                    {
                        if (Aprobar == false && validaNomina.Estado != EstadoNomina.Aprobada)
                        {
                            errores.Add(new ValidationResult("La nómina seleccionada no esta Aprobada.",
                               new[] { "SnackError" }));
                        }
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
