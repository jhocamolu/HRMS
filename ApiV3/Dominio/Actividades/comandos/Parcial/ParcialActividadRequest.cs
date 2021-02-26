using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Actividades.comandos.Parcial
{
    public class ParcialActividadRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public int? PromedioProductividad { get; set; }

        public string Descripcion { get; set; }

        public bool? Activo { get; set; }
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
                if (Codigo != null)
                {
                    var validaCodigo = dbContexto.Actividades.FirstOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                    if (validaCodigo != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El código que intentas guardar ya existe.",
                            new[] { "Codigo" }));
                    }
                }
                #endregion
                #region Nombre
                //Valida que nombre sea único
                if (Nombre != null)
                {
                    var validaNombre = dbContexto.Actividades.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                    if (validaNombre != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El nombre que intentas guardar ya existe.",
                            new[] { "Nombre" }));
                    }
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
