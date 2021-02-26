using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.NotificacionDestinatarios.Comandos.Crear
{
    public class CrearNotificacionDestinatarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? NotificacionId { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }

        [EmailAddress(ErrorMessage = ConstantesErrores.CorreoElectronico)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(EstadoNotificacion), ErrorMessage = ConstantesErrores.Emun)]
        public EstadoNotificacion Estado { get; set; }
        #endregion

        #region Validacion Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                NominaDbContext contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region NotificacionId
                Notificacion notificacionId = contexto.Notificaciones.FirstOrDefault(x => x.Id == NotificacionId);
                if (notificacionId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("notificación"), new[] { "NotificacionId" }));
                }
                else if (notificacionId.EstadoRegistro == EstadoRegistro.Eliminado)
                {
                    errores.Add(new ValidationResult("No se puede crear un destinatario a una notificación Eliminada.", new[] { "NotificacionId" }));
                }
                #endregion

                #region FuncionarioId
                if (FuncionarioId != null)
                {
                    Funcionario funcionarioId = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                    if (funcionarioId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"), new[] { "FuncionarioId" }));
                    }
                    else
                    {
                        var destinatario = contexto.NotificacionDestinatarios
                                                   .FirstOrDefault(x => x.FuncionarioId == FuncionarioId &&
                                                                        x.NotificacionId == NotificacionId);
                        if (destinatario != null)
                        {
                            errores.Add(new ValidationResult("El funcionario que intentas ingresar ya se encuentra registrado.", new[] { "FuncionarioId" }));
                        }
                    }
                }
                if (CorreoElectronico != null)
                {
                    var correoDestinatario = contexto.NotificacionDestinatarios
                                               .FirstOrDefault(x => x.CorreoElectronico == CorreoElectronico &&
                                                                    x.NotificacionId == NotificacionId);

                    if (correoDestinatario != null)
                    {
                        errores.Add(new ValidationResult("El correo electrónico que intentas ingresar ya se encuentra registrado.", new[] { "CorreoElectronico" }));
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
