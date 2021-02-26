using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoGastoViajes.Comandos.Actualizar
{
    public class ActualizarTipoGastoViajeRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ConceptoNominaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public TipoGastosViaje? Tipo { get; set; }

        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaTipo = dbContexto.TipoGastoViajes.FirstOrDefault(x => x.Tipo == Tipo &&
                                                                                x.Id != Id);
                if (validaTipo != null)
                {
                    errores.Add(new ValidationResult(
                        $"El tipo de gasto de viaje que intentas guardar ya existe.",
                        new[] { "Tipo" }));
                }
                else
                {

                    var validaTipoConcepto = dbContexto.TipoGastoViajes.FirstOrDefault(x => x.Id != Id &&
                                                                                            x.Tipo == Tipo &&
                                                                                            x.ConceptoNominaId == ConceptoNominaId);
                    if (validaTipoConcepto != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El tipo de gasto de viaje que intentas guardar ya existe.",
                            new[] { "Tipo" }));
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
