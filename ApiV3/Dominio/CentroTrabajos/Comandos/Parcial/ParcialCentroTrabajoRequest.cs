using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CentroTrabajos.Comandos.Estado
{
    public class ParcialCentroTrabajoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(9, ErrorMessage = ConstantesErrores.Maximo + " 9.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + " 40.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + " ]+$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }

        public double PorcentajeRiesgo { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region id
                var elemento = dbContexto.CentroTrabajos.FirstOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
                #endregion
                #region Codigo
                if (Codigo != null)
                {
                    //Valida que código sea único
                    var validaCodigo = dbContexto.CentroTrabajos.FirstOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                    if (validaCodigo != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El centro de trabajo que intentas guardar ya existe.",
                            new[] { "Codigo" }));
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
