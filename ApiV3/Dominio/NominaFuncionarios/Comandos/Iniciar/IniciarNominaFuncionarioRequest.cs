using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Iniciar
{
    public class IniciarNominaFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int NominaId { get; set; }

        public List<int> NominaFuncionario { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaNomina = dbContexto.Nominas.FirstOrDefault(x => x.Id == NominaId);
                if (validaNomina == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("nómina"),
                       new[] { "NominaId" }));
                    return errores;
                }

                // Valida si la nomina se encuentra en estado de EnLiquidacion
                if (validaNomina.Estado == EstadoNomina.EnLiquidacion)
                {
                    errores.Add(new ValidationResult("La nómina que intentas calcular se encuentra en proceso de liquidación.",
                      new[] { "NominaId" }));
                }
                else
                {
                    // Valida si la nomina se encuentra en estado de EnLiquidacion o si existe algun funcionario en estado Pendiente o EnLiquidacion
                    var nominaFuncionario = dbContexto.NominaFuncionarios.Where(x => x.NominaId == NominaId &&
                                                                    (x.Estado == EstadoNominaFuncionario.EnLiquidacion ||
                                                                    x.Estado == EstadoNominaFuncionario.Pendiente))
                                .ToList();
                    if (nominaFuncionario.Any())
                    {
                        errores.Add(new ValidationResult("La nómina que intentas calcular se encuentra en proceso de liquidación.",
                    new[] { "NominaId" }));
                    }
                }

                if (!NominaFuncionario.Any())
                {
                    errores.Add(new ValidationResult("No se ha seleccionado ningún funcionario para realizar el cálculo.",
                     new[] { "Snack" }));
                }

                var validaFuncionario = dbContexto.NominaFuncionarios.Where(z => NominaFuncionario.Contains(z.Id)).ToList();
                ;
                if (validaFuncionario.Count < NominaFuncionario.Count)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("nómina funcionario"),
                   new[] { "NominaFuncionarioId" }));
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
