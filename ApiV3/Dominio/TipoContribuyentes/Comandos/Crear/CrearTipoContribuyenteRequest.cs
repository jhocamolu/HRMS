using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoContribuyentes.Comandos.Crear
{
    public class CrearTipoContribuyenteRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Codigo
        [MaxLength(2, ErrorMessage = ConstantesErrores.Maximo + " 2")]
        [Range(1, 99, ErrorMessage = ConstantesErrores.Rango + "1 - 99.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }
        #endregion
        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + "/ ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion
        #region TipoPersonaContribuyente
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(TipoPersonaContribuyente), ErrorMessage = "No es un tipo de contribuyente válido.")]
        public TipoPersonaContribuyente Persona { get; set; }
        #endregion
        #endregion
        #region Validaciones Manueales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Valida Unico
                var validaUnico = contexto.TipoContribuyentes.FirstOrDefault(x => x.Codigo == Codigo
                                                                                && x.Nombre == Nombre
                                                                                && x.Persona == Persona);

                if (validaUnico != null)
                {
                    errores.Add(new ValidationResult(
                       $"El tipo de contribuyente que intentas guardar para este tipo de persona ya existe.",
                       new[] { "Nombre" }));
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
