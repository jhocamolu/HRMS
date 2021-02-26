using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Contratos.Comandos.Finalizar
{
    public class FinalizarContratoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CausalTerminacionId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaTerminacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string ObservacionFinalizacionContrato { get; set; }

        #endregion
        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region existeID
                var existeID = contexto.Contratos.FirstOrDefault(x => x.Id == Id);
                if (existeID == null)
                {
                    errores.Add(new ValidationResult("No existe Id.", new[] { "Id" }));
                    return errores;
                }
                #endregion
                #region CentroOperativoId
                if (CausalTerminacionId != null)
                {
                    var causalTerminacionId = contexto.CausalTerminaciones.FirstOrDefault(x => x.Id == CausalTerminacionId);
                    if (causalTerminacionId == null)
                    {
                        errores.Add(new ValidationResult("No existe una causal con los datos ingresados.",
                            new[] { "CausalTerminacionId" }));
                    }
                }
                #endregion
                // Obtener datos actuales del contrato.
                var maxOtrosi = contexto.ContratoOtroSis.Where(x => x.ContratoId == Id &&
                                                                        x.FechaAplicacion <= DateTime.Today &&
                                                                        x.EstadoRegistro == EstadoRegistro.Activo)
                                                    .OrderByDescending(c => c.FechaAplicacion)
                                                    .FirstOrDefault();
                dynamic contratoActual;
                //retorna valores otroSi
                if (maxOtrosi != null)
                {
                    var camposOtroSi = new
                    {
                        maxOtrosi.FechaFinalizacion
                    };
                    contratoActual = camposOtroSi;
                }
                //retorna Valores contrato
                else
                {
                    var contrato = contexto.Contratos.FirstOrDefault(x => x.Id == Id);
                    var camposContrato = new
                    {
                        contrato.FechaFinalizacion
                    };
                    contratoActual = camposContrato;
                }

                #region FechaTerminacion
                /***CA02 Comentariado para pruebas
                if ((DateTime)FechaTerminacion.Value.Date == contratoActual.FechaFinalizacion.Date)
                {
                    errores.Add(new ValidationResult("La fecha de terminación no puede ser igual que la fecha de finalización del contrato.",
                                       new[] { "FechaTerminacion" }));
                }
                ******/

                if ((DateTime)FechaTerminacion.Value.Date > contratoActual.FechaFinalizacion.Date)
                {
                    errores.Add(new ValidationResult("La fecha de terminación no puede ser mayor que la fecha de finalización del contrato.",
                                       new[] { "FechaTerminacion" }));
                }

                //Primero obtenemos el día actual
                DateTime actual = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime primerDiaDelMes = new DateTime(actual.Year, actual.Month, 1);

                /** CA01 Comentariado para pruebas
                if ((DateTime)FechaTerminacion.Value.Date < primerDiaDelMes.Date)
                {
                    errores.Add(new ValidationResult("La fecha de terminación no puede ser diferente a una fecha del mes en curso.",
                                       new[] { "FechaTerminacion" }));
                }
                ***/
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
