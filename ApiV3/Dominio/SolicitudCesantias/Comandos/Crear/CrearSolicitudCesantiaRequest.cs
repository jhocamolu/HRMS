using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Crear
{
    public class CrearSolicitudCesantiaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
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
                var validaExistaEnTramite = dbContexto.SolicitudCesantias.FirstOrDefault(x => x.FuncionarioId == FuncionarioId &&
                                                                                        x.Estado == EstadoCesantia.EnTramite &&
                                                                                        x.EstadoRegistro == EstadoRegistro.Activo);
                if (validaExistaEnTramite != null)
                {
                    errores.Add(new ValidationResult("Ya existe una solicitud de anticipo de cesantías en trámite.",
                           new[] { "snack" }));
                }
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
