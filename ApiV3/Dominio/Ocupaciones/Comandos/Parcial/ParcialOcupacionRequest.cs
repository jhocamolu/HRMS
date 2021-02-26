using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Ocupaciones.Comandos.Parcial
{
    public class ParcialOcupacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(4, ErrorMessage = ConstantesErrores.Maximo + " 4.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }

        [MaxLength(200, ErrorMessage = ConstantesErrores.Maximo + " 200.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + "]+", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region id
                var elemento = dbContexto.Ocupaciones.SingleOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
                #endregion
                #region Codigo

                //Valida que código sea único
                if (Codigo != null || Nombre != null)
                {
                    elemento = dbContexto.Ocupaciones.SingleOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                    if (elemento != null)
                    {
                        errores.Add(new ValidationResult(
                            $"La ocupación que intentas guardar ya existe.",
                           new[] { "Codigo" }));
                    }

                    var elementoNombre = dbContexto.Ocupaciones.SingleOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                    if (elementoNombre != null)
                    {
                        errores.Add(new ValidationResult(
                            $"La ocupación que intentas guardar ya existe.",
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
    }
}
