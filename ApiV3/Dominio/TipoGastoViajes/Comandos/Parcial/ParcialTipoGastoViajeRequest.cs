using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoGastoViajes.Comandos.Parcial
{
    public class ParcialTipoGastoViajeRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        public int Id { get; set; }

        public int? ConceptoNominaId { get; set; }

        public TipoGastosViaje? Tipo { get; set; }

        public bool? Activo { get; set; }

        #endregion
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                if (Tipo != null)
                {

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
                        if (Tipo != null && ConceptoNominaId != null)
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
