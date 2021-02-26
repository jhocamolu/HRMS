using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Funcionarios.Consulta
{
    public class ObtenerPDFContratoFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var validaUnico = contexto.FuncionarioDatoActuales.FirstOrDefault(x => x.Id == FuncionarioId);
                if (validaUnico == null)
                {
                    errores.Add(new ValidationResult($"{ConstantesErrores.NoExiste("el funcionario")}",
                       new[] { "SnackError" }));
                }
                var contrato = contexto.Contratos.FirstOrDefault(x => x.Id == validaUnico.ContratoId);
                var tipocontrato = contexto.TipoContratos.FirstOrDefault(y => y.Id == contrato.TipoContratoId);
                if (string.IsNullOrEmpty(tipocontrato.DocumentoSlug))
                {
                    errores.Add(new ValidationResult($"El tipo de contrato no tiene una plantilla relacionada.",
                       new[] { "SnackError" }));
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
