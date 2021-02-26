using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Dependencias.Comandos.Crear
{
    public class CrearDependenciaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Codigo
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + "]+$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + ConstantesExpresionesRegulares.Guion + "]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion

        #region DependenciapadreId, para crear relacion con jerarquia
        public int? DependenciaPadreId { get; set; }
        #endregion

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                #region Codigo

                //Valida que código sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var elemento = dbContexto.Dependencias.SingleOrDefault(x => x.Codigo == Codigo);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El código que intentas guardar ya existe.",
                        new[] { "Codigo" }));
                }
                #endregion

                #region Nombre

                //Valida que Nombre sea único
                elemento = dbContexto.Dependencias.SingleOrDefault(x => x.Nombre == Nombre);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre que intentas guardar ya existe.",
                        new[] { "Nombre" }));
                }
                #endregion

                #region DependenciaPadreId
                if (DependenciaPadreId == null)
                {
                    var padre = dbContexto.DependenciaJerarquias.FirstOrDefault(x => x.DependenciaPadreId == DependenciaPadreId);
                    if (padre != null)
                    {
                        errores.Add(new ValidationResult(
                            $"La dependencia que intentas guardar debe tener una dependencia padre.",
                            new[] { "DependenciaPadreId" }));
                    }
                }
                else
                {
                    var padre = dbContexto.Dependencias.FirstOrDefault(x => x.Id == DependenciaPadreId);
                    if (padre == null)
                    {
                        errores.Add(new ValidationResult(
                            $"No existe esta dependencia padre.",
                            new[] { "DependenciaPadreId" }));
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
