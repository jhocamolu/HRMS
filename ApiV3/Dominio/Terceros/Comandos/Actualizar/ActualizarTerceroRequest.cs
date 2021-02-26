using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static ApiV3.Infraestructura.Utilidades.DigitoVerificacion;

namespace ApiV3.Dominio.Terceros.Comandos.Actualizar
{
    public class ActualizarTerceroRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + "60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                    ConstantesExpresionesRegulares.Espacio + "]*$",
                    ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(20, ErrorMessage = ConstantesErrores.Maximo + "20.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Nit { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]$", ErrorMessage = ConstantesErrores.Rango + "0 - 9.")]
        public int? DigitoVerificacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? DivisionPoliticaNivel2Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [MinLength(7, ErrorMessage = ConstantesErrores.Minimo + "7.")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + "15.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? EntidadFinancieraId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoCuentaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(20, ErrorMessage = ConstantesErrores.Maximo + "20.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string NumeroCuenta { get; set; }

        [MaxLength(300, ErrorMessage = ConstantesErrores.Maximo + "300.")]
        public string Descripcion { get; set; }
        #endregion

        #region validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            NominaDbContext contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            try
            {
                #region Id
                Tercero existeId = contexto.Terceros.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("Tercero"),
                        new[] { "snack" }));
                    return errores;
                }
                #endregion

                #region Nombre
                Tercero nombre = contexto.Terceros.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (nombre != null)
                {
                    errores.Add(new ValidationResult("El nombre del tercero que intentas guardar ya existe.",
                        new[] { "Nombre" }));
                }
                #endregion

                #region Nit
                Tercero nit = contexto.Terceros.FirstOrDefault(x => x.Nit == Nit && x.Id != Id);
                if (nit != null)
                {
                    errores.Add(new ValidationResult("El número de identificación del tercero que intentas guardar ya existe.",
                        new[] { "Nit" }));
                }
                #endregion

                #region DigitoVerificacion
                if (DigitoVerificacion != null && Nit != null)
                {
                    var digitoCalculado = CalcularDigitoVerificacion(Nit.ToString());
                    if (DigitoVerificacion.ToString() != digitoCalculado)
                    {
                        errores.Add(new ValidationResult("El digito verificación que intentas guardar no es correcto, debería ser " + digitoCalculado,
                            new[] { "DigitoVerificacion" }));
                    }
                }
                #endregion

                #region DivisionPoliticaNivel2Id
                DivisionPoliticaNivel2 municipio = contexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == DivisionPoliticaNivel2Id);
                if (municipio == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("Municipio"),
                               new[] { "DivisionPoliticaNivel2Id" }));
                }
                #endregion

                #region EntidadFinancieraId
                EntidadFinanciera entidadFinanciera = contexto.EntidadFinancieras.FirstOrDefault(x => x.Id == EntidadFinancieraId);
                if (entidadFinanciera == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("EntidadFinanciera"),
                        new[] { "EntidadFinancieraId" }));
                }
                #endregion

                #region TipoCuentaId
                TipoCuenta tipoCuenta = contexto.TipoCuentas.FirstOrDefault(x => x.Id == TipoCuentaId);
                if (tipoCuenta == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("Tipo cuenta"),
                        new[] { "TipoCuentaId" }));
                }
                #endregion
            }
            catch (System.Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
        #endregion
    }
}
