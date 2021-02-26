using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Notificaciones.Comandos.Parcial
{
    public class ParcialNotificacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }


        [EnumDataType(typeof(TipoNotificacion), ErrorMessage = ConstantesErrores.Emun)]
        public TipoNotificacion? Tipo { get; set; }



        [MaxLength(200, ErrorMessage = ConstantesErrores.Maximo + "200.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico
                                 + ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Titulo { get; set; }

        public string Mensaje { get; set; }

        public bool EnEjecucion { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var funcionario = contexto.Notificaciones.FirstOrDefault(x => x.Id == Id);
                if (funcionario == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("Notificaion"),
                        new[] { "Id" }));
                    return errores;
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
