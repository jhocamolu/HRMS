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

namespace ApiV3.Dominio.ActividadFuncionarios.Comandos.Crear
{
    public class CrearActividadFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaFin { get; set; }

        public DateTime? FechaInicio { get; set; }

        #endregion
        #region Validaciones
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                if ((DateTime)FechaFin.Value.Date > DateTime.Now.Date )
                {
                    errores.Add(new ValidationResult(
                       $"La fecha de corte que intentas ingresar no puede ser mayor que la fecha actual.",
                       new[] { "FechaFin" }));
                }

                //Obtener fecha de inicial
                var verificaExistenRegistros = dbContexto.ActividadFuncionarios.Where(x=> x.EstadoRegistro == EstadoRegistro.Activo)
                                                                                .Max(x => x.FechaFinalizacion);
                if (verificaExistenRegistros != null)
                {
                    FechaInicio = verificaExistenRegistros.Value;
                }
                else
                {
                    // Consulta subperiodo por defecto
                    var tipoPeriodoPorDefecto = dbContexto.SubPeriodos.Include(x=>x.TipoPeriodo).FirstOrDefault(x=> x.TipoPeriodo.PagoPorDefecto == true);

                    if (tipoPeriodoPorDefecto != null)
                    {
                        //tipoPeriodoPorDefecto.Dias
                        var fechaFinCalcular = (DateTime)FechaFin;
                        FechaInicio = fechaFinCalcular.AddDays(-tipoPeriodoPorDefecto.Dias+1);
                    }
                    else
                    {
                        errores.Add(new ValidationResult(
                        $"Ningún tipo de período se encuentra como pago por defecto.",
                        new[] { "FechaFin" }));
                    }
                }
                if (FechaFin <= FechaInicio)
                {
                    errores.Add(new ValidationResult(
                    $"La fecha de corte que intentas ingresar no puede ser menor a la última fecha de obtención de actividades.",
                    new[] { "FechaFin" }));

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
