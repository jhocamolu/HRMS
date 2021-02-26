using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.TipoLiquidaciones.Comandos.Crear
{
    public class ListaTipoLiquidacionModulo
    {
        public ModuloSistema Modulo { get; set; }
    }
    public class CrearTipoLiquidacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(20, ErrorMessage = ConstantesErrores.Maximo + " 20.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int TipoPeriodoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.TipoOracion + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [MaxLength(300, ErrorMessage = ConstantesErrores.Maximo + " 300.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? FechaManual { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? Contabiliza { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? AplicaPila { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(TipoLiquidacionProceso), ErrorMessage = "No es un Proceso valido.")]
        public TipoLiquidacionProceso? Proceso { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ConceptoNominaAgrupadorId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public OperacionTotalTipoLiqidacion? OperacionTotal { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public List<ListaTipoLiquidacionModulo> ListaTipoLiquidacionModulos { get; set; } = new List<ListaTipoLiquidacionModulo>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Codigo
                var validaUnico = contexto.TipoLiquidaciones.FirstOrDefault(x => x.Codigo == Codigo);
                if (validaUnico != null)
                {
                    errores.Add(new ValidationResult(
                       $"El código que intentas ingresar ya existe.",
                       new[] { "Codigo" }));
                }
                #endregion
                #region Nombre
                var validaNombre = contexto.TipoLiquidaciones.FirstOrDefault(x => x.Nombre == Nombre);
                if (validaNombre != null)
                {
                    errores.Add(new ValidationResult(
                       $"El nombre que intentas ingresar ya existe.",
                       new[] { "Nombre" }));
                }
                #endregion

                #region TipoPeriodoId
                var validaPeriodo = contexto.TipoPeriodos.FirstOrDefault(x => x.Id == TipoPeriodoId);
                if (validaPeriodo == null)
                {
                    errores.Add(new ValidationResult(
                       $"El período que intentas guardar no existe.",
                       new[] { "TipoPeriodoId" }));
                }
                #endregion

                #region ConceptoNominaAgrupadorId

                var validaConceptoNomina = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaAgrupadorId && x.ConceptoAgrupador == true);

                if (validaConceptoNomina == null)
                {
                    errores.Add(new ValidationResult(
                       $"El concepto de nómina que intentas guardar no existe o no es un concepto agrupador.",
                       new[] { "ConceptoNominaAgrupadorId" }));
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
