using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/// <summary>
/// Clase encargada de realizar validaciones para crear la entidad DocumentosFuncionarios
/// </summary>

namespace ApiV3.Dominio.DocumentoFuncionarios.Comandos.Crear
{
    public class CrearDocumentoFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int TipoSoporteId { get; set; }

        public string Comentario { get; set; }

        public string Adjunto { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region FuncionarioId
                var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                if (funcionario == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe un funcionarion con el valor ingresado.",
                        new[] { "FuncionarioId" }));
                }
                #endregion

                #region TipoSoporteId
                var tipoSoportes = contexto.TipoSoportes.FirstOrDefault(x => x.Id == TipoSoporteId);
                if (tipoSoportes == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe un tipo de soporte con el valor ingresado.",
                        new[] { "TipoSoporteId" }));
                }
                #endregion
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
