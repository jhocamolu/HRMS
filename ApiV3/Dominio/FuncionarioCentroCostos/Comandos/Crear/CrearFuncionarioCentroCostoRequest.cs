using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Dominio.FuncionarioCentroCostos.Comandos.Crear
{
    public class CrearFuncionarioCentroCostoRequest : IRequest<CommandResult>, IValidatableObject
    {
       
        #region Validacion manuales

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var consultaActividadesCentroCostos = dbContexto.ActividadFuncionarios.FirstOrDefault(x=> x.Estado == EstadoActividadFuncionario.Pendiente
                                                                                                       && x.EstadoRegistro == EstadoRegistro.Activo);
                //Consulta si se tiene actividadescentros costo sin procesar
                if (consultaActividadesCentroCostos == null)
                {
                    errores.Add(new ValidationResult(
                     $"No hay actividades pendientes para procesar, por favor obtenga actividades de costo primero para procesarlas.",
                    new[] { "DialogoError" }));
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
