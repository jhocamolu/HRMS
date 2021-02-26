using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Parcial
{
    public class ParcialDivisionPoliticaNivel2Request : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(3, ErrorMessage = ConstantesErrores.Maximo + " 3.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }

        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + ConstantesExpresionesRegulares.Guion + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public int DivisionPoliticaNivel1Id { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {

                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                var elemento = dbContexto.DivisionPoliticaNiveles2.SingleOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));

                    return errores;
                }
                #endregion

                #region DivisionPoliticaNivel1Id
                // Valida si NO existe alguna division nivel 1
                var elemento2 = dbContexto.DivisionPoliticaNiveles1.FirstOrDefault(x => x.Id == DivisionPoliticaNivel1Id);
                if (elemento2 == null)
                {
                    errores.Add(new ValidationResult(
                         $"No existe Ningun pais con el Id: {DivisionPoliticaNivel1Id} ingresado",
                         new[] { "DivisionPoliticaNivel1Id" }));
                }

                #endregion

                #region Codigo
                if (Codigo != null)
                {
                    //Valida que código sea único
                    elemento = dbContexto.DivisionPoliticaNiveles2.SingleOrDefault(x => x.Codigo == elemento2.Codigo + Codigo && x.Id != Id);
                    if (elemento != null)
                    {
                        errores.Add(new ValidationResult(
                            $"La división política que intentas guardar ya existe.",
                            new[] { "Codigo" }));
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
