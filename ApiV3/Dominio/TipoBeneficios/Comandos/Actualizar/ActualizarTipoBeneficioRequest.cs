using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoBeneficios.Comandos.Actualizar
{
    public class ActualizarTipoBeneficioRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                 ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + " 100.")]
        public string Nombre { get; set; }

        public int? ConceptoNominaDevengoId { get; set; }

        public int? ConceptoNominaDeduccionId { get; set; }

        public int? ConceptoNominaCalculoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? RequiereAprobacionJefe { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 0, maximum: 100000000, ErrorMessage = ConstantesErrores.Rango + "0 a 100000000.")]
        public double? MontoMaximo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ValorSolicitado { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? PlazoMes { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 0, maximum: 60, ErrorMessage = ConstantesErrores.Rango + "0 a 60.")]
        public int? CuotaPermitida { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? PeriodoPago { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? PermiteAuxilioEducativo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? PermisoEstudio { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? PermiteDescuentoNomina { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ValorAutorizado { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = ConstantesErrores.Rango + "0 a 1000.")]
        public int? DiasAntiguedad { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 0, maximum: 1000, ErrorMessage = ConstantesErrores.Rango + "0 a 1000.")]
        public int? VecesAnio { get; set; }

        public string Descripcion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                //Valida que código sea único
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Nombre
                var validaUnico = dbContexto.TipoBeneficios.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (validaUnico != null)
                {
                    errores.Add(new ValidationResult(
                       $"El nombre que intentas guardar ya existe.",
                       new[] { "Nombre" }));
                }
                #endregion


                #region ConceptoNomina
                var conceptoNominaDevengo = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaDevengoId && x.ClaseConceptoNomina == ClaseConceptoNomina.Devengo);
                if (ConceptoNominaDevengoId != null && conceptoNominaDevengo == null)
                {
                    errores.Add(new ValidationResult(
                        $"El concepto de nómina que intentas guardar no existe.",
                        new[] { "ConceptoNominaDevengoId" }));
                }
                var conceptoNominaDeduccion = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaDeduccionId && x.ClaseConceptoNomina == ClaseConceptoNomina.Deduccion);
                if (ConceptoNominaDeduccionId != null && conceptoNominaDeduccion == null)
                {
                    errores.Add(new ValidationResult(
                        $"El concepto de nómina que intentas guardar no existe.",
                        new[] { "ConceptoNominaDeduccionId" }));
                }
                var conceptoNominaCalculo = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaCalculoId && x.ClaseConceptoNomina == ClaseConceptoNomina.Calculo);
                if (ConceptoNominaCalculoId != null && conceptoNominaCalculo == null)
                {
                    errores.Add(new ValidationResult(
                        $"El concepto de nómina que intentas guardar no existe.",
                        new[] { "ConceptoNominaCalculoId" }));
                }
                #endregion

            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }

            return errores;
        }
    }
}
