using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoEmbargos.Comandos.Parcial
{
    public class ParcialTipoEmbargoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion 

        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public bool? SalarioMinimoEmbargable { get; set; }


        [Range(1, 100, ErrorMessage = ConstantesErrores.Rango + " 1 al 100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public sbyte? Prioridad { get; set; }


        public int? ConceptoNominaId { get; set; }

        #endregion
        #region ValidacionesManuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            try
            {
                //Valida que registro sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                if (Nombre != null)
                {
                    var elemento = dbContexto.TipoEmbargos.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                    if (elemento != null)
                    {
                        errors.Add(new ValidationResult(
                            $"El tipo de embargo que intentas guardar ya existe.",
                            new[] { "Nombre" }));
                    }
                }
                if (ConceptoNominaId != null)
                {
                    var validarConceptoNomina = dbContexto.ConceptoNominas.Find(ConceptoNominaId);
                    if (validarConceptoNomina == null)
                    {
                        errors.Add(new ValidationResult(
                            $"No existe.",
                            new[] { "ConceptoNominaId" }));
                    }
                    else
                    {
                        var validarTipoEmbargo = dbContexto.TipoEmbargoConceptoNominas.FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId && x.TipoEmbargoId != Id);
                        if (validarTipoEmbargo != null)
                        {
                            errors.Add(new ValidationResult(
                                $"Este concepto ya está asociado a un tipo de embargo.",
                                new[] { "ConceptoNominaId" }));
                        }

                        if (validarConceptoNomina.ClaseConceptoNomina != ClaseConceptoNomina.Deduccion)
                        {
                            errors.Add(new ValidationResult(
                                $"El concepto debe ser de deducción.",
                                new[] { "ConceptoNominaId" }));
                        }
                    }
                }
                if (Prioridad != null)
                {
                    var validaPrioridad = dbContexto.TipoEmbargos.FirstOrDefault(x => x.Prioridad == Prioridad && x.Id != Id);
                    if (validaPrioridad != null)
                    {
                        errors.Add(new ValidationResult(
                            $"Ya existe un tipo de embargo con ésta prioridad.",
                            new[] { "Prioridad" }));
                    }
                }
            }
            catch (Exception e)
            {
                errors.Add(new ValidationResult(e.Message));
            }
            return errors;
        }
        #endregion
    }
}
