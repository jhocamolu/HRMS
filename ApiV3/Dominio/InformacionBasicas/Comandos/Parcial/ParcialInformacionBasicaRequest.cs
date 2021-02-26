using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static ApiV3.Infraestructura.Utilidades.DigitoVerificacion;

namespace ApiV3.Dominio.InformacionBasicas.Comandos.Parcial
{
    public class ParcialInformacionBasicaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        #region Id
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
        #endregion

        #region BASICOS
        #region Nombre
        [MinLength(4, ErrorMessage = ConstantesErrores.Minimo + "4.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "128.")]
        public string Nombre { get; set; }
        #endregion

        #region Nit
        [Range(1, 999999999999999, ErrorMessage = ConstantesErrores.Rango + "10000000 - 999999999999999.")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + "15.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Nit { get; set; }
        #endregion

        #region DigitoVerificacion
        public int? DigitoVerificacion { get; set; }
        #endregion

        #region RazonSocial
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "128.")]
        public string RazonSocial { get; set; }

        #endregion RazonSocial

        #region ActividadEconomicaId
        public int? ActividadEconomicaId { get; set; }
        #endregion
        #endregion

        #region LOCALIZACION
        #region DivisionPoliticaNivel2Id
        public int? DivisionPoliticaNivel2Id { get; set; }
        #endregion

        #region Direccion
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255")]
        public string Direccion { get; set; }
        #endregion

        #region Telefono
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + "10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Telefono { get; set; }

        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + "10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Fax { get; set; }
        #endregion

        #region CorreoElectronico
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255")]
        [EmailAddress(ErrorMessage = ConstantesErrores.CorreoElectronico)]
        public string CorreoElectronico { get; set; }
        #endregion

        #region Web
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255")]
        [Url(ErrorMessage = ConstantesErrores.PaginaWeb)]
        public string Web { get; set; }
        #endregion
        #endregion

        #region EMPRESA
        [DataType(DataType.Date)]
        public DateTime? FechaConstitucion { get; set; }


        public int? TipoContribuyenteId { get; set; }


        public int? OperadorPagoId { get; set; }


        public int? ARLId { get; set; }


        public int? TipoDocumentoId { get; set; }


        public int? NaturalezaJuridicaId { get; set; }


        public int? TipoPersonaId { get; set; }


        public int? ClaseAportanteTipoAportanteId { get; set; }


        public int? CargoId { get; set; }


        public bool? BeneficiarioLey1429De2010 { get; set; }


        public bool? BeneficiarioImpuestoEquidad { get; set; }
        #endregion

        #region Estado Registro
        public bool? Activo { get; set; }
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
                    errores.Add(new ValidationResult($"No existen datos con el Id ingresado", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region Nit
                if (Nit != null)
                {
                    var informacion = contexto.InformacionBasicas.FirstOrDefault(x => x.Id != Id && x.Nit == Nit);
                    if (informacion != null)
                    {
                        errores.Add(new ValidationResult($"El número de NIT que intentas guardar ya existe", new[] { "NIT" }));
                    }

                    if (DigitoVerificacion == null)
                    {
                        errores.Add(new ValidationResult("Para actualizar el NIT, se requiere el digito de verificación",
                            new[] { "Nit" }));
                    }
                }
                #endregion

                #region DigitoVerificacion
                if (DigitoVerificacion != null)
                    if (DigitoVerificacion.ToString() != CalcularDigitoVerificacion(Nit.ToString()))
                    {
                        errores.Add(new ValidationResult("El digito verificación que intentas guardar no es correcto",
                            new[] { "DigitoVerificacion" }));
                    }
                #endregion

                #region ActividadEconomica
                if (ActividadEconomicaId != null)
                {
                    var actividadEconomica = contexto.ActividadEconomicas.FirstOrDefault(x => x.Id == ActividadEconomicaId);
                    if (actividadEconomica == null)
                    {
                        errores.Add(new ValidationResult("La actividad económica intentas guardar no existe",
                            new[] { "ActividadEconomicaId" }));
                    }
                }
                #endregion

                #region Municipio
                if (DivisionPoliticaNivel2Id != null)
                {
                    var municipio = contexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == DivisionPoliticaNivel2Id);
                    if (municipio == null)
                    {
                        errores.Add(new ValidationResult("El municipio que intentas guardar no existe",
                            new[] { "MunicipioId" }));
                    }
                }
                #endregion

                #region FechaConstitucion
                if (FechaConstitucion != null)
                    if (FechaConstitucion > fechaActual)
                    {
                        errores.Add(new ValidationResult("La fecha constitución que intentas ingresar no debe ser mayor a la fecha actual",
                            new[] { "FechaConstitucion" }));
                    }
                #endregion

                #region TipoContribuyenteId
                if (TipoContribuyenteId != null)
                {
                    var tipoContribuyente = contexto.TipoContribuyentes.FirstOrDefault(x => x.Id == TipoContribuyenteId);
                    if (tipoContribuyente == null)
                    {
                        errores.Add(new ValidationResult("El tipo contribuyente que intentas guardar no existe",
                            new[] { "TipoContribuyenteId" }));
                    }
                }
                #endregion

                #region OperadorPagoId
                if (OperadorPagoId != null)
                {
                    var operadorPago = contexto.OperadorPagos.FirstOrDefault(x => x.Id == OperadorPagoId);
                    if (operadorPago == null)
                    {
                        errores.Add(new ValidationResult("El operador de pago que intentas guardar no existe",
                            new[] { "OperadorPagoId" }));
                    }
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
