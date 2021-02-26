using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ConceptoNominaCuentaContables.Parcial
{
    public class ParcialConceptoNominaCuentaContableRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? Id { get; set; }

        //[Required(ErrorMessage = ConstantesErrores.Requerido)]
        //[RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        //public int? ConceptoNominaId { get; set; }


        //[RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        //public int? CentroCostoId { get; set; }


        //[RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        //public int? CuentaContableId { get; set; }

        #region Estado_Registro
        public bool? Activo { get; set; }
        #endregion
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                #region existeId
                var existeId = contexto.ConceptoNominaCuentaContables.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult("No existe datos con el Id ingresado.",
                        new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region ConceptoNominaId
                //var conceptoNominaId = contexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId);
                //if (conceptoNominaId == null)
                //{
                //    errores.Add(new ValidationResult("No existe un concepto nómina con los datos ingresados.",
                //        new[] { "ConceptoNominaId" }));
                //}
                #endregion

                #region CentroCostoId, CuentaContableId
                //if (CentroCostoId != null && CuentaContableId == null)
                //{
                //    errores.Add(new ValidationResult("Se requiere la cuenta contable, para actualizar el centro de costos.",
                //       new[] { "CuentaContableId" }));
                //}
                //if (CentroCostoId == null && CuentaContableId != null)
                //{
                //    errores.Add(new ValidationResult("Se requiere el centro de costos, para actualizar la cuenta contable.",
                //       new[] { "CentroCostoId" }));
                //}
                //if (CentroCostoId != null && CuentaContableId != null)
                //{
                //    var centroCostoId = contexto.CentroCostos.FirstOrDefault(x => x.Id == CentroCostoId);
                //    if (centroCostoId == null)
                //    {
                //        errores.Add(new ValidationResult("No existe un centro de costo con los datos ingresados.",
                //            new[] { "CentroCostoId" }));
                //    }

                //    var cuentaContableId = contexto.CuentaContables.FirstOrDefault(x => x.Id == CuentaContableId);
                //    if (cuentaContableId == null)
                //    {
                //        errores.Add(new ValidationResult("No existe una cuenta contable con los datos ingresados.",
                //            new[] { "CuentaContableId" }));
                //    }

                //    if (centroCostoId != null && cuentaContableId != null)
                //    {
                //        var centroCostoUnico = contexto.ConceptoNominaCuentaContables
                //                            .FirstOrDefault(x => x.Id != Id &&
                //                                            x.ConceptoNominaId == ConceptoNominaId &&
                //                                            x.CentroCostoId == CentroCostoId &&
                //                                            x.CuentaContableId == CuentaContableId);
                //        if (centroCostoUnico != null)
                //        {
                //            errores.Add(new ValidationResult("El centro de costo y cuenta contable que intentas guardar, ya se encuentran registrados.",
                //                new[] { "CentroCostoId" }));
                //        }
                //    }
                //}
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
