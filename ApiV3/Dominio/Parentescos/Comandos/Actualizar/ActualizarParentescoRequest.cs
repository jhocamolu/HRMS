using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Parentescos.Comandos.Actualizar
{
    public class ActualizarParentescoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(TipoParentescos), ErrorMessage = "No es un tipo de parentesco válido.")]
        public TipoParentescos Tipo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(GradoParentescos), ErrorMessage = "No es un grado de parentesco válido.")]
        public GradoParentescos Grado { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + "/ ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 99, ErrorMessage = ConstantesErrores.Rango + "1 - 99.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public int NumeroPersonasPermitidas { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                //Valida que registro sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                var elemento = dbContexto.Parentescos.FirstOrDefault(x => x.Nombre == Nombre
                                                                     && x.Tipo == Tipo
                                                                     && x.Grado == Grado
                                                                     && x.Id != Id);

                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El  parentesco  que intentas guardar ya existe",
                        new[] { "Nombre" }));
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
