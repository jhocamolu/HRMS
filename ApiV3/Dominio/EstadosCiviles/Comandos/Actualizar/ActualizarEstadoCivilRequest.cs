using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.EstadosCiviles.Comandos.Actualizar
{
    public class ActualizarEstadoCivilRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 40.")]
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.Slash + "]+", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Nombre
                //Valida que nombre sea único
                var elemento = dbContexto.EstadoCiviles.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El estado civil que intentas guardar ya existe.",
                        new[] { "Nombre" }));
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
