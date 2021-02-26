using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Parcial
{
    public class ParcialSolicitudCesantiaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        public int Id { get; set; }

        public int? FuncionarioId { get; set; }

        public int? MotivoSolicitudCesantiaId { get; set; }

        public double? ValorSolicitado { get; set; }

        public string Soporte { get; set; }

        public string Observacion { get; set; }

        #endregion
        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region FuncionarioId
                if (FuncionarioId != null)
                {
                    var validaFuncionario = dbContexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                    if (validaFuncionario == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"),
                           new[] { "FuncionarioId" }));
                    }
                }
                #endregion
                #region MotivoSolicitudId
                if (MotivoSolicitudCesantiaId != null)
                {
                    var validaFuncionario = dbContexto.MotivoSolicitudCesantias.FirstOrDefault(x => x.Id == MotivoSolicitudCesantiaId);
                    if (validaFuncionario == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("motivo solicitud cesantia"),
                           new[] { "MotivoSolicitudCesantiaId" }));
                    }
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
