using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CuantaBancaria.Comandos.Crear
{
    public class CrearCuentaBancariaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? EntidadFinancieraId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoCuentaId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.TipoOracion
                                 + ConstantesExpresionesRegulares.Numerico
                                 + ConstantesExpresionesRegulares.Parentesis
                                 + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Numero { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + " 255.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.TipoOracion
                                 + ConstantesExpresionesRegulares.Numerico
                                 + ConstantesExpresionesRegulares.Parentesis
                                 + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region EntidadFinancieraId
                var entidadFinancieraId = dbContexto.EntidadFinancieras.FirstOrDefault(x => x.Id == EntidadFinancieraId);
                if (entidadFinancieraId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("entidad financiera"),
                        new[] { "EntidadFinancieraId" }));
                }
                #endregion

                #region TipoCuentaId
                var tipoCuentaId = dbContexto.TipoCuentas.FirstOrDefault(x => x.Id == TipoCuentaId);
                if (tipoCuentaId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo cuenta"),
                        new[] { "TipoCuentaId" }));
                }
                #endregion

                #region Numero
                var numero = dbContexto.CuentaBancarias.FirstOrDefault(x => x.Numero == Numero);
                if (numero != null)
                {
                    errores.Add(new ValidationResult(
                        $"El numero que intentas guardar ya existe.",
                        new[] { "Numero" }));
                }
                #endregion

                #region Nombre
                var nombre = dbContexto.CuentaBancarias.FirstOrDefault(x => x.Nombre == Nombre);
                if (nombre != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre que intentas guardar ya existe.",
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
