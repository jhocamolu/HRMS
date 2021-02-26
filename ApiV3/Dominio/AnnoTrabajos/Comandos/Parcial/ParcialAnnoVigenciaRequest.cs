using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.AnnoTrabajos.Comandos.Parcial
{
    public class ParcialAnnoVigenciaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public EstadoAnnoVigencia? Estado { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                if (Estado == EstadoAnnoVigencia.Cerrado)
                {
                    // Valida el estado a Cerrado
                    var validaEstadoCerrado = dbContexto.AnnoVigencias.Where(x => x.Estado == EstadoAnnoVigencia.Vigente &&
                                                                               x.EstadoRegistro == EstadoRegistro.Activo)
                                                                               .ToList();

                    if (validaEstadoCerrado.Count == 1)
                    {
                        errores.Add(new ValidationResult(
                            $"No es posible cambiar el estado debido a que siempre debe existir un año de trabajo en estado vigente.",
                            new[] { "snack" }));

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
