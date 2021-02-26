using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ConceptoNominaCuentaContables.Crear
{
    public class CrearConceptoNominaCuentaContableRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? ConceptoNominaId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroCostoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CuentaContableId { get; set; }
        #endregion

        #region Validacion Manueles
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                #region ConceptoNominaId
                var conceptoNominaId = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId);
                if (conceptoNominaId == null)
                {
                    errores.Add(new ValidationResult("No existe un concepto nómina con los datos ingresados.",
                        new[] { "ConceptoNominaId" }));
                    return errores;
                }
                else
                {

                    if (conceptoNominaId.OrigenCentroCosto == OrigenCentroCostoNomina.Especifico)
                    {
                        var soloUnRegistro = contexto.ConceptoNominaCuentaContables
                                        .FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId);
                        if (soloUnRegistro != null)
                        {
                            errores.Add(new ValidationResult("Sólo se permite ingresar un registro, cuando el origen de centro de costo es específico.",
                                new[] { "ConceptoNominaId" }));
                            return errores;
                        }
                    }
                }
                #endregion

                #region CuentaContableId
                var cuentaContable = contexto.CuentaContables.FirstOrDefault(x => x.Id == CuentaContableId);
                if (cuentaContable == null)
                {
                    errores.Add(new ValidationResult("No existe una cuenta contable con los datos ingresados.",
                        new[] { "CuentaContableId" }));
                }
                if (cuentaContable.Naturaleza == NaturalezaContable.Debito && CentroCostoId == null)
                {
                    errores.Add(new ValidationResult("No se debe ingresar una cuenta contable débito sin centro de costo.",
                        new[] { "CentroCostoId" }));
                }
                if (cuentaContable.Naturaleza == NaturalezaContable.Credito && CentroCostoId != null)
                {
                    errores.Add(new ValidationResult("No se debe ingresar una cuenta contable crédito con centro de costo.",
                        new[] { "CentroCostoId" }));
                }
                #endregion

                #region CentroCostoId

                if (CentroCostoId != null)
                {
                    var centroCosto = contexto.CentroCostos.FirstOrDefault(x => x.Id == CentroCostoId);
                    if (centroCosto == null)
                    {
                        errores.Add(new ValidationResult("No existe un centro de costo con los datos ingresados.",
                            new[] { "CentroCostoId" }));
                    }
                }
                #endregion

                #region ExisteRelacion
                var centroCostoUnico = contexto.ConceptoNominaCuentaContables
                                    .FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId &&
                                                    x.CentroCostoId == CentroCostoId &&
                                                    x.CuentaContableId == CuentaContableId);
                if (centroCostoUnico != null)
                {
                    errores.Add(new ValidationResult("El centro de costo y cuenta contable que intentas guardar, ya se encuentran registrados.",
                        new[] { "CentroCostoId" }));
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
