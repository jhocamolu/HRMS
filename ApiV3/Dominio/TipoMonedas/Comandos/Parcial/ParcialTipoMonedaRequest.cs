using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoMonedas.Comandos.Parcial
{
    public class ParcialTipoMonedaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(4, ErrorMessage = ConstantesErrores.Maximo + " 4.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + "]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Codigo { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + " ]+$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }

        public bool? Activo { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region id
                var elemento = dbContexto.TipoMonedas.FirstOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
                #endregion
                //Valida que Código sea único
                if (Codigo != null)
                {

                    //Valida que Código 
                    var validaUnico = dbContexto.TipoMonedas.FirstOrDefault(x => x.Codigo == Codigo && x.Id != Id);


                    if (validaUnico != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El tipo de moneda que intentas guardar ya existe.",
                            new[] { "Codigo" }));
                    }

                }
                if (Nombre != null)
                {
                    //Valida que nombre sea único

                    var element = dbContexto.TipoMonedas.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);

                    if (element != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El nombre de moneda que intentas guardar ya existe.",
                            new[] { "Nombre" }));
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
