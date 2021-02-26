using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.FuncionarioEstudios.Comandos.Eliminar
{
    public class EliminarFuncionarioEstudioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            try
            {
                #region Id
                var existe = dbContexto.FuncionarioEstudios.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {

                    errores.Add(new ValidationResult(
                        $"No existe este estudio.",
                        new[] { "Id" }));
                }
                #endregion

            }
            catch
            {

            }
            return errores;
        }
        #endregion
    }
}
