using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Actualizar
{


    public class ActualizarSolicitudCesantiaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? MotivoSolicitudCesantiaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 999999999, ErrorMessage = ConstantesErrores.Rango + "1 - 999999999.")]
        public double? ValorSolicitado { get; set; }

        public string Soporte { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
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
                #region Estado
                var solicitud = dbContexto.SolicitudCesantias.FirstOrDefault(x => x.Id == Id);
                if (solicitud == null)
                {
                    errores.Add(new ValidationResult("No existe.",
                                              new[] { "Id" }));
                }
                else
                {
                    if (solicitud.Estado != EstadoCesantia.EnTramite)
                    {
                        errores.Add(new ValidationResult("La solicitud no puede ser modificada en el estado en el que se encuentra.",
                           new[] { "errror" }));
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
