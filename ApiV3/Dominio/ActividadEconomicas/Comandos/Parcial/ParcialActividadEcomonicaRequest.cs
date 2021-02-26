using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ActividadEconomicas.Comandos.Parcial
{
    public class ParcialActividadEcomonicaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(4, ErrorMessage = ConstantesErrores.Maximo + " 4.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        public string Nombre { get; set; }

        public bool? Activo { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                //Elemento no existe
                var existe = dbContexto.ActividadEconomicas.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe",
                        new[] { "Id" }));
                    return errores;
                }

                //Valida que Código sea único
                var element = dbContexto.ActividadEconomicas.FirstOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                if (element != null)
                {
                    errores.Add(new ValidationResult(
                        $"El Código de la actividad económica que intentas guardar ya existe.",
                        new[] { "Codigo" }));
                }
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
    }
}
