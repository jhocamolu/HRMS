using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CargoGrados.Parcial
{
    public class ParcialCargoGradoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + " 40.")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int? CargoId { get; set; }

        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                if (Nombre != null)
                {
                    // Valida Nombre único
                    var existeCargoGrado = dbContexto.CargoGrados.FirstOrDefault(x => x.Nombre == Nombre && x.CargoId == CargoId && x.Id != Id);
                    if (existeCargoGrado != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El nombre del grado que intentas guardar ya existe.",
                            new[] { "Nombre" }));
                    }
                }

                if (CargoId != null)
                {
                    //Valida Cargo Exista 
                    var existeCargo = dbContexto.Cargos.FirstOrDefault(x => x.Id == CargoId);
                    if (existeCargo == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El cargo no existe.",
                            new[] { "CargoId" }));
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
