using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Contratos.Comandos.Parcial
{
    public class ParcialContratoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        #region DatoContrato
        public int? FuncionarioId { get; set; }


        public int? TipoContratoId { get; set; }

        public string NumeroContrato { get; set; }


        public int? CargoDependenciaId { get; set; }

        public int? GrupoNominaId { get; set; }

        public DateTime? FechaInicio { get; set; }


        public DateTime? FechaFinalizacion { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? PeriodoPrueba { get; set; }
        #endregion

        #region DatosPago
        public double? Sueldo { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroOperativoId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? DivisionPoliticaNivel2Id { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroCostoId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FormaPagoId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoMonedaId { get; set; }



        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? EntidadFinancieraId { get; set; }



        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoCuentaId { get; set; }



        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string NumeroCuenta { get; set; }
        #endregion

        #region Otros
        public bool? RecibeDotacion { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? JornadaLaboralId { get; set; }


        public bool? EmpleadoConfianza { get; set; }


        public ProcedimientoRetenciones? ProcedimientoRetencion { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroTrabajoId { get; set; }

        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500")]
        public string Observaciones { get; set; }


        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500")]
        public string Justificacion { get; set; }
        #endregion

        #region Activo
        public bool? Activo { get; set; }
        #endregion

        #region Cancelar
        public bool? Cancelar { get; set; }
        #endregion
        #endregion

        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region existeID
                var existeID = contexto.Contratos.FirstOrDefault(x => x.Id == Id);
                if (existeID == null)
                {
                    errores.Add(new ValidationResult("No existe Id.", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region NumeroCuenta
                if (NumeroCuenta != null)
                {
                    var numeroCuenta = contexto.Contratos.FirstOrDefault(x =>
                    x.NumeroCuenta == NumeroCuenta && x.FuncionarioId != FuncionarioId);

                    if (numeroCuenta != null)
                    {
                        errores.Add(new ValidationResult("El número de cuenta ingresado, ya existe.",
                            new[] { "NumeroCuenta" }));
                    }
                }
                #endregion

                #region CentroOperativoId
                if (CentroOperativoId != null)
                {
                    var centroOperativoId = contexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoId);
                    if (centroOperativoId == null)
                    {
                        errores.Add(new ValidationResult("No existe un centro operativo con los datos ingresados.",
                            new[] { "CentroOperativoId" }));
                    }
                }
                #endregion

                #region DivisionPoliticaNivel2Id
                if (DivisionPoliticaNivel2Id != null)
                {
                    var divisionPoliticaNivel2ContratoId = contexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == DivisionPoliticaNivel2Id);
                    if (divisionPoliticaNivel2ContratoId == null)
                    {
                        errores.Add(new ValidationResult("No existe un municipio con los datos ingresados.",
                            new[] { "DivisionPoliticaNivel2ContratoId" }));
                    }
                }
                //DivisionPoliticaNivel2ContratoId
                #endregion

                #region CentroCostoId
                if (CentroCostoId != null)
                {
                    var centroCostoId = contexto.CentroCostos.FirstOrDefault(x => x.Id == CentroCostoId);
                    if (centroCostoId == null)
                    {
                        errores.Add(new ValidationResult("No existe un centro de costo con los datos ingresados.",
                            new[] { "CentroCostoId" }));
                    }
                }
                #endregion

                #region FormaPagoId
                if (FormaPagoId != null)
                {
                    var formaPagoId = contexto.FormaPagos.FirstOrDefault(x => x.Id == FormaPagoId);
                    if (formaPagoId == null)
                    {
                        errores.Add(new ValidationResult("No existe un forma de pago con los datos ingresados.",
                            new[] { "FormaPagoId" }));
                    }
                }
                #endregion()|

                #region TipoMonedaId
                if (TipoMonedaId != null)
                {
                    var tipoMonedaId = contexto.TipoMonedas.FirstOrDefault(x => x.Id == TipoMonedaId);
                    if (tipoMonedaId == null)
                    {
                        errores.Add(new ValidationResult("No existe un centro de costo con los datos ingresados.",
                            new[] { "TipoMonedaId" }));
                    }
                }
                #endregion

                #region EntidadFinancieraId
                if (EntidadFinancieraId != null)
                {
                    var entidadFinancieraId = contexto.EntidadFinancieras.FirstOrDefault(x => x.Id == EntidadFinancieraId);
                    if (entidadFinancieraId == null)
                    {
                        errores.Add(new ValidationResult("No existe un entidad financiera con los datos ingresados.",
                            new[] { "EntidadFinancieraId" }));
                    }
                }
                #endregion

                #region GrupoNomina
                if (GrupoNominaId != null)
                {
                    var validaGrupoNomina = contexto.GrupoNominas.FirstOrDefault(x => x.Id == GrupoNominaId);
                    if (validaGrupoNomina == null)
                    {
                        errores.Add(new ValidationResult("No existe.",
                            new[] { "GrupoNomina" }));
                    }
                }

                #endregion

                #region TipoCuentaId
                if (TipoCuentaId != null)
                {
                    var tipoCuentaId = contexto.TipoCuentas.FirstOrDefault(x => x.Id == TipoCuentaId);
                    if (tipoCuentaId == null)
                    {
                        errores.Add(new ValidationResult("No existe un tipo cuenta con los datos ingresados.",
                            new[] { "TipoCuentaId" }));
                    }
                }
                #endregion

                #region JornadaLaboralId
                if (JornadaLaboralId != null)
                {
                    var jornadaLaboralId = contexto.JornadaLaborales.FirstOrDefault(x => x.Id == JornadaLaboralId);
                    if (jornadaLaboralId == null)
                    {
                        errores.Add(new ValidationResult("No existe jornada laboral con los datos ingresados.",
                            new[] { "JornadaLaboralId" }));
                    }
                }
                #endregion

                #region CentroTrabajoId
                if (CentroTrabajoId != null)
                {
                    var centroTrabajoId = contexto.CentroTrabajos.FirstOrDefault(x => x.Id == CentroTrabajoId);
                    if (centroTrabajoId == null)
                    {
                        errores.Add(new ValidationResult("No existe jornada laboral con los datos ingresados.",
                            new[] { "CentroTrabajoId" }));
                    }
                }
                #endregion

                #region Cancelar
                if (Cancelar != null)
                {
                    if (Cancelar == true && Justificacion == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                                  new[] { "Justificacion" }));
                    }
                    if (Cancelar == true && existeID.Estado != EstadoContrato.SinIniciar)
                    {
                        errores.Add(new ValidationResult("Solo se pude cancelar un contrato en estado sin inicar.",
                                 new[] { "snack" }));
                    }
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
