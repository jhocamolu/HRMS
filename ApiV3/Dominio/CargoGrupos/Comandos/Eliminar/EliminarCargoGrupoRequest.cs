using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CargoGrupos.Comandos.Eliminar
{
    public class EliminarCargoGrupoRequest : IRequest<CommandResult>, IValidatableObject
    {
        public int Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var validaCargoContrato = dbContexto.Contratos.FirstOrDefault(x => x.CargoGrupoId == Id);
                if (validaCargoContrato != null)
                {
                    errores.Add(new ValidationResult(
                          $"No es posible eliminar este registro debido a que se encuentra relacionado con más información en el sistema.",
                          new[] { "snack" }));
                }

                var cargoGrupo = dbContexto.CargoGrupos.FirstOrDefault(x => x.Id == Id);

                //Obtiene todos los grupos creados para el cargo
                var maxCargoGrupos = dbContexto.CargoGrupos.Where(x => x.CargoId == cargoGrupo.CargoId)
                                                        .OrderByDescending(x => x.Grupo.Orden)
                                                        .FirstOrDefault();

                if (maxCargoGrupos != null)
                {
                    if (Id != maxCargoGrupos.Id)
                    {
                        errores.Add(new ValidationResult(
                           $"No se puede eliminar un grupo con un orden inferior.",
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
    }
}
