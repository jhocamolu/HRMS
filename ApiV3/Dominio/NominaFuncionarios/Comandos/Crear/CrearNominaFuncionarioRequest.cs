using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Crear
{
    public class CrearNominaFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int NominaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public List<int> Funcionarios { get; set; }

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
                }

                foreach (var item in Funcionarios)
                {

                    var validaFuncionario = dbContexto.Funcionarios.FirstOrDefault(x => x.Id == item);
                    if (validaFuncionario == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"),
                       new[] { "Funcionarios" }));
                        return errores;
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
