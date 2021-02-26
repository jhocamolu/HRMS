using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CargoPresupuestos.Comandos.Actualizar
{
    public class ActualizarCargoPresupuestoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? AnnoVigenciaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 100, ErrorMessage = ConstantesErrores.Rango + " 1 - 100.")]
        public int? Cantidad { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Código
                //Valida que código sea único

                var validaAnio = dbContexto.CargoPresupuestos.FirstOrDefault(x => x.AnnoVigenciaId == AnnoVigenciaId &&
                                                                                x.CargoId == CargoId &&
                                                                                x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                x.Id != Id);

                if (validaAnio != null)
                {
                    errores.Add(new ValidationResult(
                        $"El año que intentas ingresar ya existe.",
                        new[] { "AnnoVigenciaId" }));
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
