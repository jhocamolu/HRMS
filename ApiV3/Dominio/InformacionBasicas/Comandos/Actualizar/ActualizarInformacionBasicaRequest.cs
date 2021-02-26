using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static ApiV3.Infraestructura.Utilidades.DigitoVerificacion;

namespace ApiV3.Dominio.InformacionBasicas.Comandos.Actualizar
{
    public class ActualizarInformacionBasicaRequest : IRequest<CommandResult>, IValidatableObject
    {

        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region BASICOS
        #region Nombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(4, ErrorMessage = ConstantesErrores.Minimo + "4.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "128.")]
        public string Nombre { get; set; }
        #endregion

        #region Nit
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + "15.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Nit { get; set; }
        #endregion

        #region DigitoVerificacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? DigitoVerificacion { get; set; }
        #endregion

        #region RazonSocial
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(4, ErrorMessage = ConstantesErrores.Minimo + "4.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "128.")]
        public string RazonSocial { get; set; }

        #endregion RazonSocial

        #region ActividadEconomicaId
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ActividadEconomicaId { get; set; }
        #endregion
        #endregion

        #region LOCALIZACION
        #region DivisionPoliticaNivel2Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? DivisionPoliticaNivel2Id { get; set; }
        #endregion

        #region Direccion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Direccion { get; set; }
        #endregion

        #region Telefono
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + "10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + "10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Fax { get; set; }
        #endregion

        #region CorreoElectronico
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        [EmailAddress(ErrorMessage = ConstantesErrores.CorreoElectronico)]
        public string CorreoElectronico { get; set; }
        #endregion

        #region Web
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        [Url(ErrorMessage = ConstantesErrores.PaginaWeb)]
        public string Web { get; set; }
        #endregion
        #endregion

        #region EMPRESA
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [DataType(DataType.Date)]
        public DateTime? FechaConstitucion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoContribuyenteId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? OperadorPagoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ARLId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoDocumentoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? NaturalezaJuridicaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoPersonaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? ClaseAportanteTipoAportanteId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? BeneficiarioLey1429De2010 { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? BeneficiarioImpuestoEquidad { get; set; }
        #endregion

        #endregion

        #region Validaciones_Manueales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                //Variable fecha para validaciones
                DateTime fechaActual = DateTime.Today;

                #region Id
                var id = contexto.InformacionBasicas.FirstOrDefault(x => x.Id == Id);
                if (id == null)
                {
                    errores.Add(new ValidationResult($"No existen datos con el Id ingresado.", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region Nit
                var informacion = contexto.InformacionBasicas.FirstOrDefault(x => x.Id != Id && x.Nit == Nit);
                if (informacion != null)
                {
                    errores.Add(new ValidationResult($"El número de NIT que intentas guardar ya existe.", new[] { "Nit" }));
                }
                #endregion

                #region DigitoVerificacion
                if (DigitoVerificacion.ToString() != CalcularDigitoVerificacion(Nit.ToString()))
                {
                    errores.Add(new ValidationResult("DV Incorrecto.",
                        new[] { "DigitoVerificacion" }));
                }
                #endregion

                #region ActividadEconomica
                var actividadEconomica = contexto.ActividadEconomicas.FirstOrDefault(x => x.Id == ActividadEconomicaId);
                if (actividadEconomica == null)
                {
                    errores.Add(new ValidationResult("La actividad económica intentas guardar no existe.",
                        new[] { "ActividadEconomicaId" }));
                }
                #endregion



                //telefono no tiene validacion manual
                //correo no tiene validacion manual

                #region FechaConstitucion
                if (FechaConstitucion > fechaActual)
                {
                    errores.Add(new ValidationResult("La fecha constitución que intentas ingresar no debe ser mayor a la fecha actual.",
                        new[] { "FechaConstitucion" }));
                }
                #endregion

                #region TipoContribuyenteId
                var tipoContribuyente = contexto.TipoContribuyentes.FirstOrDefault(x => x.Id == TipoContribuyenteId);
                if (tipoContribuyente == null)
                {
                    errores.Add(new ValidationResult("El tipo contribuyente que intentas guardar no existe.",
                        new[] { "TipoContribuyenteId" }));
                }
                #endregion

                #region OperadorPagoId
                var operadorPago = contexto.OperadorPagos.FirstOrDefault(x => x.Id == OperadorPagoId);
                if (operadorPago == null)
                {
                    errores.Add(new ValidationResult("El operador de pago que intentas guardar no existe.",
                        new[] { "OperadorPagoId" }));
                }
                #endregion

                #region TipoDocumentoId
                var tipoDocumento = contexto.TipoDocumentos.FirstOrDefault(x => x.Id == TipoDocumentoId);
                if (tipoDocumento == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo de documento"),
                        new[] { "TipoDocumentoId" }));
                }
                #endregion

                #region NaturalezaJuridicaId
                var naturalezaJuridica = contexto.NaturalezaJuridicas.FirstOrDefault(x => x.Id == NaturalezaJuridicaId);
                if (naturalezaJuridica == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("naturaleza jurídica"),
                        new[] { "NaturalezaJuridicaId" }));
                }
                #endregion

                #region TipoPersonaId
                var tipoPersona = contexto.TipoPersonas.FirstOrDefault(x => x.Id == TipoPersonaId);
                if (tipoPersona == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo de persona"),
                        new[] { "TipoPersonaId" }));
                }
                #endregion

                #region ClaseAportanteTipoAportanteId
                var claseAportanteTipoAportante = contexto.ClaseAportanteTipoAportantes.FirstOrDefault(x => x.Id == ClaseAportanteTipoAportanteId);
                if (claseAportanteTipoAportante == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("clase aportante tipo aportante"),
                        new[] { "ClaseAportanteTipoAportanteId" }));
                }
                #endregion

                #region CargoId
                var cargo = contexto.Cargos.FirstOrDefault(x => x.Id == CargoId);
                if (cargo == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("cargo"),
                        new[] { "CargoId" }));
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
