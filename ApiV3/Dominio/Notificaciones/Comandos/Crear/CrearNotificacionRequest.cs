using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Notificaciones.Comandos.Crear
{
    public class NotificacionDestinatarios
    {
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }

        [EmailAddress(ErrorMessage = ConstantesErrores.CorreoElectronico)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string CorreoElectronico { get; set; }
    }
    public class CrearNotificacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(TipoNotificacion), ErrorMessage = ConstantesErrores.Emun)]
        public TipoNotificacion? Tipo { get; set; }


        public DateTime? Fecha { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(200, ErrorMessage = ConstantesErrores.Maximo + "200.")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Mensaje { get; set; }


        public List<NotificacionDestinatarios> NotificacionDestinatarios { get; set; } = new List<NotificacionDestinatarios>();
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                if (NotificacionDestinatarios != null)
                {
                    foreach (var item in NotificacionDestinatarios)
                    {
                        if (item.FuncionarioId != null)
                        {
                            var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.Id == item.FuncionarioId);
                            if (funcionario == null)
                            {
                                errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"),
                                    new[] { "notificacionDestinatarios" }));
                            }
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
        #endregion
    }
}
