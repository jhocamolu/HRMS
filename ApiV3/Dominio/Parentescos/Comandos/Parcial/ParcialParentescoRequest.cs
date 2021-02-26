using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Parentescos.Comandos.Estado
{
    public class ParcialParentescoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [EnumDataType(typeof(TipoParentescos), ErrorMessage = "No es un tipo de parentesco válido.")]
        public TipoParentescos? Tipo { get; set; }

        [EnumDataType(typeof(GradoParentescos), ErrorMessage = "No es un grado de parentesco válido.")]
        public GradoParentescos? Grado { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + "/ ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [Range(1, 99, ErrorMessage = ConstantesErrores.Rango + "1 - 99.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? NumeroPersonasPermitidas { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region id
                var elemento = dbContexto.Parentescos.SingleOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
                }
                #endregion
                if (Nombre != null && Tipo != null && Grado != null)
                {
                    //Valida que registro sea único
                    var validaExiste = dbContexto.Parentescos.FirstOrDefault(x => x.Nombre == Nombre
                                                                         && x.Tipo == Tipo
                                                                         && x.Grado == Grado
                                                                        );

                    if (validaExiste != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El  parentesco  que intentas guardar ya existe",
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
