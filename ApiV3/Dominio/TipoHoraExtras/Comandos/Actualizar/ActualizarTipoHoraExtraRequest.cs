using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoHoraExtras.Comandos.Actualizar
{
    public class ActualizarTipoHoraExtraRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(TipoHoraExtra), ErrorMessage = "No es un tipo de hora extra valido.")]
        public TipoHoraExtra Tipo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int ConceptoNominaId { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                //Elemento no existe
                var existe = dbContexto.TipoHoraExtras.SingleOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe",
                        new[] { "id" }));
                    return errores;
                }

                var concepto = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId);
                if (concepto == null)
                {
                    errores.Add(new ValidationResult(
                        $"El concepto de nómina que intentas guardar no existe.",
                        new[] { "ConceptoNominaId" }));
                    return errores;
                }

                var elemento = dbContexto.TipoHoraExtras.SingleOrDefault(x => x.Tipo == Tipo && x.ConceptoNominaId == ConceptoNominaId && x.Id != Id);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El tipo de hora extra que intentas guardar ya existe.",
                        new[] { "Tipo" }));
                    return errores;
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
