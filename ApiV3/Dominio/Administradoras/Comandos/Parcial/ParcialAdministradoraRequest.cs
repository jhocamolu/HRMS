using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Administradoras.Comandos.Estado
{
    public class ParcialAdministradoraRequest : IRequest<CommandResult>, IValidatableObject
    {


        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }


        public bool? Activo { get; set; }


        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        public string Codigo { get; set; }


        [MaxLength(11, ErrorMessage = ConstantesErrores.Maximo + " 11.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Nit { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Dv { get; set; }


        [MaxLength(200, ErrorMessage = ConstantesErrores.Maximo + " 200.")]
        public string Nombre { get; set; }

        public int? TipoAdministradoraId { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region id
                var existe = dbContexto.Administradoras.SingleOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe.",
                       new[] { "Id" }));
                }
                #endregion

                #region Codigo
                var codigo = dbContexto.Administradoras.SingleOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                if (codigo != null)
                {
                    errores.Add(new ValidationResult(
                        $"Ya existe una Administradora con el código: {Codigo} ingresado.",
                        new[] { "Codigo" }));
                }
                #endregion

                #region TipoAdministradoraId
                if (TipoAdministradoraId != null)
                {
                    var tipoAdministradoraId = dbContexto.TipoAdministradoras.SingleOrDefault(x => x.Id == TipoAdministradoraId);
                    if (tipoAdministradoraId == null)
                    {
                        errores.Add(new ValidationResult(
                            $"No existe este tipo de administradora.",
                            new[] { "TipoAdministradoraId" }));
                    }
                }
                #endregion

                #region DigitoVerificacion
                if (Dv != null && Dv != DigitoVerificacion.CalcularDigitoVerificacion(Nit))
                {
                    errores.Add(new ValidationResult(
                        $"El digito de verificación no corresponde al NIT registrado : {Dv} , Sugerido {DigitoVerificacion.CalcularDigitoVerificacion(Nit)}. ",
                        new[] { "Dv" }));
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
