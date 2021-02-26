using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Contratos.Comandos.Actualizar
{
    public class ActualizarContratoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        #region DatoContrato
        public int FuncionarioId { get; set; }


        public int TipoContratoId { get; set; }

        public string NumeroContrato { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 0, maximum: 100, ErrorMessage = ConstantesErrores.Rango + "0 - 100.")]
        public int? PeriodoPrueba { get; set; }

        public int GrupoNominaId { get; set; }


        public DateTime? FechaInicio { get; set; }


        public DateTime? FechaFinalizacion { get; set; }
        #endregion

        #region DatosPago
        public double Sueldo { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CargoGrupoId { get; set; }


        public int CentroOperativoId { get; set; }


        public int DivisionPoliticaNivel2Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int CentroCostoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int FormaPagoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int TipoMonedaId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? EntidadFinancieraId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoPeriodoId { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoCuentaId { get; set; }



        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string NumeroCuenta { get; set; }
        #endregion

        #region Otros
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool RecibeDotacion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int JornadaLaboralId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool EmpleadoConfianza { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public ProcedimientoRetenciones? ProcedimientoRetencion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoCotizanteSubtipoCotizanteId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ExtranjeroNoObligadoACotizarAPension { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ColombianoEnElExterior { get; set; }
        #endregion 
        #endregion

        #region Validacion Manual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                DateTime hoy = DateTime.Today;
                bool indefinido = false;
                int duracionTipoContrato = 0;

                #region existeID
                var existeID = contexto.Contratos.FirstOrDefault(x => x.Id == Id);
                if (existeID == null)
                {
                    errores.Add(new ValidationResult("No existe Id.", new[] { "Id" }));
                    return errores;
                }
                #endregion


                #region TipoContratoId
                var tipoContratoId = contexto.TipoContratos.Where(x => x.Id == TipoContratoId);
                if (tipoContratoId == null)
                {
                    errores.Add(new ValidationResult("No existe un tipo contrato con los datos ingresados.",
                        new[] { "TipoContratoId" }));
                }
                else
                {
                    indefinido = tipoContratoId.Select(x => x.TerminoIndefinido).FirstOrDefault();
                    duracionTipoContrato = (int)tipoContratoId.Select(x => x.DuracionMaxima).FirstOrDefault();
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

                #region CentroCostoId
                var centroCostoId = contexto.CentroCostos.FirstOrDefault(x => x.Id == CentroCostoId);
                if (centroCostoId == null)
                {
                    errores.Add(new ValidationResult("No existe un centro de costo con los datos ingresados.",
                        new[] { "CentroCostoId" }));
                }
                #endregion

                #region FormaPagoId
                var formaPagoId = contexto.FormaPagos.FirstOrDefault(x => x.Id == FormaPagoId);
                if (formaPagoId == null)
                {
                    errores.Add(new ValidationResult("No existe un forma de pago con los datos ingresados.",
                        new[] { "FormaPagoId" }));
                }
                #endregion()|

                #region TipoMonedaId
                var tipoMonedaId = contexto.TipoMonedas.FirstOrDefault(x => x.Id == TipoMonedaId);
                if (tipoMonedaId == null)
                {
                    errores.Add(new ValidationResult("No existe un centro de costo con los datos ingresados.",
                        new[] { "TipoMonedaId" }));
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

                #region TipoPeriodoId
                if (TipoPeriodoId != null)
                {
                    var tipoPeriodoId = contexto.TipoPeriodos.FirstOrDefault(x => x.Id == TipoPeriodoId);
                    if (tipoPeriodoId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo periodo"),
                            new[] { "TipoPeriodoId" }));
                    }
                }
                #endregion

                #region GrupoNomina
                var validaGrupoNomina = contexto.GrupoNominas.FirstOrDefault(x => x.Id == GrupoNominaId);
                if (validaGrupoNomina == null)
                {
                    errores.Add(new ValidationResult("No existe.",
                        new[] { "GrupoNomina" }));
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
                var jornadaLaboralId = contexto.JornadaLaborales.FirstOrDefault(x => x.Id == JornadaLaboralId);
                if (jornadaLaboralId == null)
                {
                    errores.Add(new ValidationResult("No existe jornada laboral con los datos ingresados.",
                        new[] { "JornadaLaboralId" }));
                }
                #endregion

                //EmpleadoConfianza
                //ProcedimientoRetencio
                
                //EstadoContrato

                // NumeroContrato

                #region TipoCotizanteSubtipoCotizanteId
                if (TipoCotizanteSubtipoCotizanteId != null)
                {
                    var tipoCotizanteSubtipoCotizantes = contexto.TipoCotizanteSubtipoCotizantes.FirstOrDefault(x => x.Id == TipoCotizanteSubtipoCotizanteId);
                    if (tipoCotizanteSubtipoCotizantes == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo cotizante,subtipo cotizante"),
                            new[] { "TipoCotizanteSubtipoCotizanteId" }));
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
