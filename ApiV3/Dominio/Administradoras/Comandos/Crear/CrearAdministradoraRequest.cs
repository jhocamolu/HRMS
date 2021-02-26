using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Administradoras.Comandos.Crear
{
    public class CrearAdministradoraRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        public string Codigo { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + "15.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Nit { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Dv { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(200, ErrorMessage = ConstantesErrores.Maximo + " 200.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoAdministradoraId { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                #region Codigo
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var codigo = dbContexto.Administradoras.SingleOrDefault(x => x.Codigo == Codigo);
                if (codigo != null)
                {
                    errores.Add(new ValidationResult(
                        "El código que intentas guardar ya existe",
                        new[] { "Codigo" }));
                }
                #endregion

                #region TipoAdministradoraId                
                var tipoAdministradoraId = dbContexto.TipoAdministradoras.SingleOrDefault(x => x.Id == TipoAdministradoraId);
                if (tipoAdministradoraId == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe este tipo de administradora.",
                        new[] { "TipoAdministradoraId" }));
                }
                #endregion

                #region DigitoVerificacion
                if (Dv != DigitoVerificacion.CalcularDigitoVerificacion(Nit))
                {
                    errores.Add(new ValidationResult(
                        $"El digito verificación que intentas guardar no es correcto.",
                        new[] { "Dv" }));
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