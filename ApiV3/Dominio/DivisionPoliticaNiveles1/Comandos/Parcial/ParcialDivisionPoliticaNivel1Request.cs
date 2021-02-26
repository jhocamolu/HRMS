using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Parcial
{
    public class ParcialDivisionPoliticaNivel1Request : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(3, ErrorMessage = ConstantesErrores.Maximo + " 3.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }

        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + ConstantesExpresionesRegulares.Espacio + ConstantesExpresionesRegulares.SignosPuntuacion + ConstantesExpresionesRegulares.Guion + "]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public int PaisId { get; set; }
        #endregion
        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {

                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                var elemento = dbContexto.DivisionPoliticaNiveles1.SingleOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));

                    return errores;
                }
                #endregion

                #region Codigo
                if (Codigo != null)
                {
                    //Valida que código sea único
                    elemento = dbContexto.DivisionPoliticaNiveles1.SingleOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                    if (elemento != null)
                    {
                        errores.Add(new ValidationResult(
                            $"La división política que intentas guardar ya existe.",
                            new[] { "Codigo" }));
                    }
                }
                #endregion

                #region PaisId
                if (PaisId != 0)
                {
                    // Valida si NO existe algun pais
                    var elemento2 = dbContexto.Paises.FirstOrDefault(x => x.Id == PaisId);
                    if (elemento2 == null)
                    {
                        errores.Add(new ValidationResult(
                             $"No existe Ningun pais con el Id: {PaisId} ingresado",
                             new[] { "PaisId" }));
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
