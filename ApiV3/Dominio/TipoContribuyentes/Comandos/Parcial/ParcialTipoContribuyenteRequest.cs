using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoContribuyentes.Comandos.Parcial
{
    public class ParcialTipoContribuyenteRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion
        #region Activo
        public bool? Activo { get; set; }
        #endregion
        #region Codigo

        [Range(1, 99, ErrorMessage = ConstantesErrores.Rango + "1 - 99.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }
        #endregion
        #region Nombre

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + "/ ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }
        #endregion
        #region TipoPersonaContribuyente

        public TipoPersonaContribuyente? Persona { get; set; }
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
                if (Codigo != null && Nombre != null && Persona != null)
                {
                    var validaUnico = contexto.TipoContribuyentes.FirstOrDefault(x => x.Codigo == Codigo
                                                                                && x.Nombre == Nombre
                                                                                && x.Persona == Persona
                                                                                && x.Id != Id);

                    if (validaUnico != null)
                    {
                        errores.Add(new ValidationResult(
                           $"El tipo de contribuyente que intentas guardar para este tipo de persona ya existe.",
                           new[] { "Nombre" }));
                    }
                }
                #endregion
                #region Existe
                var existe = contexto.TipoContribuyentes.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { "Id" }));
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
