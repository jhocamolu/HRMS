using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ContratoCentroTrabajos.Comandos.Actualizar
{
    public class ActualizarContratoCentroTrabajoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? ContratoId { get; set; }
        
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroTrabajoId { get; set; }
        
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFinal { get; set; }

        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500.")]
        public string Observacion { get; set; }

        #endregion

        #region Validacion Manual

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region existe centro trabajo
                var existeCentroTrabajo = contexto.CentroTrabajos.FirstOrDefault(x => x.Id == CentroTrabajoId);
                if (existeCentroTrabajo == null)
                {
                    errores.Add(new ValidationResult("No existe centro trabajo", new[] { "Id" }));
                }
                #endregion

                #region existe id
                var existeId = contexto.ContratoCentroTrabajos.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult("No existe Id", new[] { "Id" }));
                    return errores;
                }
                
                #endregion

                #region existe contrato
                var existeContrato = contexto.Contratos.FirstOrDefault(x => x.Id == ContratoId);
                if (existeContrato == null)
                {
                    errores.Add(new ValidationResult("No existe contrato", new[] { "Id" }));
                }
                
                #endregion

                #region FechaInicio
                // Valida que la pila tenga el mismo mes y año 
                FechaFinal = (DateTime)FechaInicio;
                var validarNominaTipoPila = contexto.Nominas.Include(x=> x.TipoLiquidacion)
                                                            .Where(x=> x.TipoLiquidacion.Codigo == "PILA")
                                                            .FirstOrDefault(x=> x.FechaInicio.Year == FechaFinal.Value.Year &&
                                                                               x.FechaInicio.Month == FechaFinal.Value.Month);
                if (validarNominaTipoPila != null)
                {
                    errores.Add(new ValidationResult("No es posible realizarse el cambio en la fecha de aplicación ingresada para el funcionario.",
                           new[] { "snackError" }));
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
