using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CargoDependencias.Comandos.Crear
{
    public class CrearCargoDependenciaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int CargoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int DependenciaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                //Valida CargoReporta Exista 
                var existeCargo = dbContexto.Cargos.FirstOrDefault(x => x.Id == CargoId);
                if (existeCargo == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe.",
                        new[] { "CargoId" }));
                }


                //Valida DependenciaId exista
                var existeDependencia = dbContexto.Dependencias.FirstOrDefault(x => x.Id == DependenciaId);
                if (existeDependencia == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe.",
                        new[] { "DependenciaId" }));
                }

                //Valida que la relación sea unica
                var existe = dbContexto.CargoDependencias.FirstOrDefault(x => x.CargoId == CargoId
                                                                        && x.DependenciaId == DependenciaId
                                                                        && x.EstadoRegistro == EstadoRegistro.Activo);
                if (existe != null)
                {
                    errores.Add(new ValidationResult(
                        $"La dependencia que intentas guardar ya existe.",
                        new[] { "DependenciaId" }));
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
