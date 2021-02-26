using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ApiV3.Dominio.Contratos.Comandos.Crear
{


    public class CrearContratosRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        #region DatoContrato
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoContratoId { get; set; }

        public string NumeroContrato { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CargoDependenciaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 0, maximum: 100, ErrorMessage = ConstantesErrores.Rango + "0 - 100.")]
        public int? PeriodoPrueba { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? GrupoNominaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]

        public DateTime? FechaInicio { get; set; }



        public DateTime? FechaFinalizacion { get; set; }
        #endregion

        #region DatosPago
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 1, maximum: 9999999999999, ErrorMessage = ConstantesErrores.Rango + "1 - 9999999999999.")]
        public double? Sueldo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CargoGrupoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroOperativoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? DivisionPoliticaNivel2Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroCostoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FormaPagoId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoMonedaId { get; set; }



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
        public bool? RecibeDotacion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? JornadaLaboralId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? EmpleadoConfianza { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public ProcedimientoRetenciones? ProcedimientoRetencion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CentroTrabajoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoCotizanteSubtipoCotizanteId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ExtranjeroNoObligadoACotizarAPension { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ColombianoEnElExterior { get; set; }
        #endregion

        //lista con los datos de la administradora
        #region Administradora Contrato
        //Administradora de Fondos de Cesantías
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Afp { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? AfPFechaInicio { get; set; }


        //Administradora de Fondos de Pensiones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FondoCesantias { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FondoCesantiasFechaInicio { get; set; }


        //Cajas de Compensación Familiar
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? CajaCompensacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? CajaCompensacionFechaInicio { get; set; }


        //Empresa Promotoras de Salud
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Eps { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? EpsFechaInicio { get; set; }
        #endregion

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                DateTime hoy = DateTime.Today;

                string identificacion = null;
                bool indefinido = false;
                int duracionTipoContrato = 0;

                #region FuncionarioId
                var funcionarioId = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                if (funcionarioId == null)
                {
                    errores.Add(new ValidationResult("No existe un funcionario con los datos ingresados.",
                        new[] { "FuncionarioId" }));
                }
                else
                {
                    identificacion = funcionarioId.NumeroDocumento;

                    var contratoActivoFuncionario = contexto.Contratos
                        .Where(x => x.FuncionarioId == FuncionarioId
                        && (x.Estado == EstadoContrato.Vigente
                        || x.Estado == EstadoContrato.SinIniciar
                        || x.Estado == EstadoContrato.PendientePorLiquidar))
                        .FirstOrDefault();
                    if (contratoActivoFuncionario != null)
                    {
                        errores.Add(new ValidationResult("El funcionario que ingresaste tiene un contrato vigente, pendiente por liquidar o sin iniciar.",
                            new[] { "FuncionarioId" }));
                        return errores;
                    }
                    else
                    {
                        var consultaNumeroContratos = contexto.Contratos
                        .Where(x => x.FuncionarioId == FuncionarioId)
                        .Count() + 1;

                        if (consultaNumeroContratos <= 9)
                        {
                            NumeroContrato = identificacion + "-0" + consultaNumeroContratos;
                        }

                        NumeroContrato = identificacion + "-" + consultaNumeroContratos;
                    }
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


                #region CargoDependencia
                var validaCargoDependencia = contexto.CargoDependencias.FirstOrDefault(x => x.Id == CargoDependenciaId);
                if (validaCargoDependencia == null)
                {
                    errores.Add(new ValidationResult("No existe un cargo - dependencia con los datos ingresados.",
                        new[] { "CargoDependenciaId" }));
                }
                #endregion

                #region FechaInicio Comentado -3 días y mayo 5 días
                /*if (FechaInicio < hoy.AddDays(-3))
                {
                    errores.Add(new ValidationResult(Fecha.NoPuedeSerMenor("fecha inicio", "3 días", "fecha actual"),
                        new[] { "FechaInicio" }));
                }
                else if (FechaInicio > hoy.AddDays(5))
                {
                    errores.Add(new ValidationResult(Fecha.NoPuedeSerMayor("fecha de inicio", "5 días", "fecha actual"),
                        new[] { "FechaInicio" }));
                }*/
                #endregion

                #region FechaFinalizacion
                if (indefinido == false || FechaInicio != null)
                {
                    if (FechaFinalizacion == null && indefinido == false )
                    {
                        errores.Add(new ValidationResult("Requerido", new[] { "FechaFinalizacion" }));
                    }
                    else
                    {
                        if (FechaInicio != null)
                        {
                            var fechaInicio = (DateTime)FechaInicio;

                            if (FechaFinalizacion > fechaInicio.AddDays(duracionTipoContrato))
                            {
                                errores.Add(new ValidationResult("La fecha de finalización supera el tiempo permitido para el tipo de contrato.",
                                new[] { "FechaFinalizacion" }));
                            }
                            if (FechaFinalizacion < FechaInicio)
                            {
                                errores.Add(new ValidationResult(Fecha.NoPuedeSerMenor("fecha de finalización", "", "fecha de inicio del contrato"),
                                    new[] { "FechaFinalizacion" }));
                            }   
                        }
                    }
                }
                #endregion

                #region CargoGrupoId
                CargoGrupo cargoGrupoId = null;
                if (CargoGrupoId != null)
                {
                    cargoGrupoId = contexto.CargoGrupos.FirstOrDefault(x => x.Id == CargoGrupoId);
                    if (cargoGrupoId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("grupo de trabajo"),
                            new[] { "CargoGrupoId" }));
                    }
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
                var centroOperativoId = contexto.CentroOperativos.FirstOrDefault(x => x.Id == CentroOperativoId);
                if (centroOperativoId == null)
                {
                    errores.Add(new ValidationResult("No existe un centro operativo con los datos ingresados.",
                        new[] { "CentroOperativoId" }));
                }
                #endregion

                #region CA12  validaCargoDependencia cargoGrupoId centroOperativoId
                if (validaCargoDependencia != null && cargoGrupoId != null && centroOperativoId != null)
                {
                    var cargoReporta = contexto
                                        .CargoReportas
                                        .FirstOrDefault(x => x.CargoDependenciaReportaId == validaCargoDependencia.Id);

                    if (cargoReporta != null && cargoReporta.JefeInmediato.Equals(true))
                    {
                        var existe = contexto.Contratos.FirstOrDefault(x => x.CentroOperativoId.Equals(CentroOperativoId) &&
                                                                           x.CargoGrupoId.Equals(CargoGrupoId) &&
                                                                           x.CargoDependenciaId.Equals(CargoDependenciaId) &&
                                                                           x.Estado == EstadoContrato.Vigente &&
                                                                           x.EstadoRegistro == EstadoRegistro.Activo
                        );
                        if (existe != null)
                        {
                            errores.Add(new ValidationResult(
                                "Ya existe un contrato para este cargo en el mismo centro operativo, " +
                                "y con el mismo grupo de trabajo. Por favor selecciona un grupo de cargo diferente.",
                        new[] { "Snack" }));
                        }
                    }
                }
                #endregion

                #region DivisionPoliticaNivel2ContratoId
                var divisionPoliticaNivel2ContratoId = contexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == DivisionPoliticaNivel2Id);
                if (divisionPoliticaNivel2ContratoId == null)
                {
                    errores.Add(new ValidationResult("No existe un municipio con los datos ingresados.",
                        new[] { "DivisionPoliticaNivel2ContratoId" }));
                }
                //DivisionPoliticaNivel2ContratoId
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

                #region CentroTrabajoId
                var centroTrabajoId = contexto.CentroTrabajos.FirstOrDefault(x => x.Id == CentroTrabajoId);
                if (centroTrabajoId == null)
                {
                    errores.Add(new ValidationResult("No existe jornada laboral con los datos ingresados.",
                        new[] { "CentroTrabajoId" }));
                }
                #endregion


                #region Afp
                if (Afp != null)
                {
                    var existeAdministradora = contexto.Administradoras.FirstOrDefault(x => x.Id == Afp);
                    if (existeAdministradora == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("administradora"),
                         new[] { "Afp" }));
                    }
                    else
                    {
                        var afpCodigo = contexto
                                        .TipoAdministradoras
                                        .FirstOrDefault(x => x.Id == existeAdministradora.TipoAdministradoraId);
                        if (afpCodigo.Codigo != "AFP")
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.NoExiste("AFP"),
                         new[] { "Afp" }));
                        }
                    }
                }
                #endregion

                #region FondoCesantias
                if (FondoCesantias != null)
                {
                    var existeAdministradora = contexto
                                                .Administradoras
                                                .FirstOrDefault(x => x.Id == FondoCesantias);
                    if (existeAdministradora == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("administradora"),
                         new[] { "FondoCesantias" }));
                    }
                    else
                    {
                        var fondoCesantiasCodigo = contexto
                                                    .TipoAdministradoras
                                                    .FirstOrDefault(x => x.Id == existeAdministradora.TipoAdministradoraId);
                        if (fondoCesantiasCodigo.Codigo != "AFC")
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.NoExiste("fondo cesantias"),
                         new[] { "FondoCesantias" }));
                        }
                    }
                }
                #endregion

                #region CajaCompensacion
                if (CajaCompensacion != null)
                {
                    var existeAdministradora = contexto.Administradoras.FirstOrDefault(x => x.Id == CajaCompensacion);
                    if (existeAdministradora == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("administradora"),
                            new[] { "CajaCompensacion" }));
                    }
                    else
                    {
                        var CajaCompensacionCodigo = contexto.TipoAdministradoras.FirstOrDefault(x => x.Id == existeAdministradora.TipoAdministradoraId);
                        if (CajaCompensacionCodigo.Codigo != "CCF")
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.NoExiste("caja compensacion"),
                                new[] { "CajaCompensacion" }));
                        }
                    }
                }
                #endregion

                #region Eps
                if (Eps != null)
                {
                    var existeAdministradora = contexto.Administradoras.FirstOrDefault(x => x.Id == Eps);
                    if (existeAdministradora == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("administradora"),
                            new[] { "Eps" }));
                    }
                    else
                    {
                        var epsCodigo = contexto
                                        .TipoAdministradoras
                                        .FirstOrDefault(x => x.Id == existeAdministradora.TipoAdministradoraId);
                        if (epsCodigo.Codigo != "EPS")
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.NoExiste("EPS"),
                                new[] { "Eps" }));
                        }
                    }
                }
                #endregion

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

