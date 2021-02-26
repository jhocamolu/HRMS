using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CargoGrupos.Comandos.Crear
{
    public class CrearCargoGrupoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoId { get; set; }

        public int? GrupoId { get; set; }

        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                // Valida si existe el proximo grupo
                if (CargoId != null)
                {
                    //Obtiene todos los grupos creados para el cargo
                    var cargoGrupos = dbContexto.CargoGrupos.Where(x => x.CargoId == CargoId)
                                                            .Select(x => x.GrupoId)
                                                            .ToArray();

                    // Obtiene el grupo que tenga el orden menor y no se encuentre en la consulta anterior.
                    var consultaGrupo = dbContexto.Grupos.Where(x => !cargoGrupos.Contains(x.Id) &&
                                                                x.EstadoRegistro == EstadoRegistro.Activo)
                                                         .OrderBy(x => x.Orden)
                                                         .FirstOrDefault();

                    if (consultaGrupo == null)
                    {
                        errores.Add(new ValidationResult(
                           $"Ya se han creado todos los grupos que pueden ir para un cargo.",
                           new[] { "snack" }));
                    }
                    else
                    {
                        GrupoId = consultaGrupo.Id;
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
