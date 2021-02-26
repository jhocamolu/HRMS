using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Beneficios.Comandos.Actualizar
{
    public class BeneficiosSubperiodos
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? SubPeriodoId { get; set; }

        public int? BeneficioId { get; set; }
    }

    public class BeneficiosAdjuntos
    {
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int TipoBeneficioRequisitoId { get; set; }

        public string AdjuntoId { get; set; }
    }

    public class ActualizarBeneficioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        #region Elementos
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoBeneficioId { get; set; }



        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaSolicitud { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 1, maximum: 99999999, ErrorMessage = ConstantesErrores.Rango + "1- 99.999.999.")]
        public double? ValorSolicitud { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 1, maximum: 20, ErrorMessage = ConstantesErrores.Rango + "1- 20.")]
        public int? PlazoMaximo { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoPeriodoId { get; set; }


        [EnumDataType(typeof(OpcionAuxilioEducativo), ErrorMessage = ConstantesErrores.Emun)]
        public OpcionAuxilioEducativo? OpcionAuxilioEducativo { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 1, maximum: 1000, ErrorMessage = ConstantesErrores.Rango + "1- 1.000.")]
        public int? CantidadHoraSemana { get; set; }


        public DateTime? FechaInicioEstudio { get; set; }


        public DateTime? FechaFinalizacionEstudio { get; set; }


        [MaxLength(300, ErrorMessage = ConstantesErrores.Maximo + ("300."))]
        public string Observacion { get; set; }


        [EnumDataType(typeof(EstadoBeneficiosCorportativos), ErrorMessage = ConstantesErrores.Emun)]
        public EstadoBeneficiosCorportativos Estado { get; set; }



        public string ObservacionAprobacion { get; set; }



        public string ObservacionAutorizacion { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 1, maximum: 99999999, ErrorMessage = ConstantesErrores.Rango + "1- 99.999.999.")]
        public double? ValorAutorizado { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public double? ValorCobrar { get; set; }
        #endregion

        #region Lista
        public List<BeneficiosSubperiodos> BeneficiosSubperiodos { get; set; } = new List<BeneficiosSubperiodos>();

        public List<BeneficiosAdjuntos> BeneficiosAdjuntos { get; set; } = new List<BeneficiosAdjuntos>();
        #endregion
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            var hoy = DateTime.Today;
            Beneficio existeId = null;
            double diasLaborados = 0;
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                #region Id
                existeId = contexto.Beneficios.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("beneficio"),
                        new[] { "Id" }));
                    return errores;
                }
                else if (existeId.Estado != EstadoBeneficiosCorportativos.EnTramite &&
                    existeId.EstadoRegistro == EstadoRegistro.Activo)
                {
                    errores.Add(new ValidationResult("La solicitud no puede ser modificada en el estado en el que se encuentra.",
                        new[] { "confirmarError" }));
                    return errores;
                }
                #endregion

                #region FuncionarioId
                if (FuncionarioId != null)
                {
                    var funcionarioId = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                    if (funcionarioId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("Funcionario"), new[] { "FuncionarioId" }));
                    }
                    else if (funcionarioId.Estado == EstadoFuncionario.Activo)
                    {
                        var datosActuales = contexto.FuncionarioDatoActuales.FirstOrDefault(x => x.Id == FuncionarioId);
                        var contrato = contexto.Contratos.FirstOrDefault(x => x.Id == datosActuales.ContratoId);
                        diasLaborados = (hoy - contrato.FechaInicio).TotalDays;

                        if (contrato.Estado == EstadoContrato.Cancelado || contrato.Estado == EstadoContrato.Terminado)
                        {
                            errores.Add(new ValidationResult(
                            "El funcionario que intentas ingresar no cuenta con un contrato vigente, por favor revisa.",
                            new[] { "FuncionarioId" }));
                            return errores;
                        }
                    }
                    else
                    {
                        errores.Add(new ValidationResult(
                            "El funcionario que intentas ingresar no se encuentra activo, por favor revisa.",
                            new[] { "FuncionarioId" }));
                        return errores;
                    }
                }
                #endregion

                #region TipoBeneficioId
                var tipoBeneficio = contexto.TipoBeneficios.FirstOrDefault(x => x.Id == TipoBeneficioId);
                if (tipoBeneficio == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo beneficio"), new[] { "TipoBeneficioId" }));
                    return errores;
                }
                else if (tipoBeneficio.DiasAntiguedad > diasLaborados)
                {
                    errores.Add(new ValidationResult(
                        "El funcionario que intentas ingresar no cumple con la condición de antigüedad para acceder al beneficio.",
                        new[] { "FuncionarioId" }));
                }
                #endregion

                #region ValorSolicitud
                if (tipoBeneficio.ValorSolicitado)
                {
                    if (ValorSolicitud == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "ValorSolicitud" }));
                    }
                    else if (ValorSolicitud > tipoBeneficio.MontoMaximo)
                    {
                        errores.Add(new ValidationResult(
                        "El valor que solicitas no debe ser mayor al establecido para el beneficio.",
                        new[] { "ValorSolicitud" }));
                    }

                }
                #endregion

                #region PlazoMaximo
                if (tipoBeneficio.PlazoMes)
                {
                    if (PlazoMaximo == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "PlazoMaximo" }));
                    }
                    else if (PlazoMaximo > tipoBeneficio.CuotaPermitida)
                    {
                        errores.Add(new ValidationResult(
                            "El plazo máximo en meses que ingresaste no debe ser mayor al establecido para el beneficio.",
                            new[] { "PlazoMaximo" }));
                    }
                }
                #endregion

                #region TipoPeriodoId
                if (tipoBeneficio.PeriodoPago)
                {
                    if (TipoPeriodoId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "TipoPeriodoId" }));
                    }
                    else
                    {
                        var tipoPeriodoId = contexto.TipoPeriodos.FirstOrDefault(x => x.Id == TipoPeriodoId);
                        if (tipoPeriodoId == null)
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo periodo"), new[] { "TipoPeriodoId" }));
                        }
                    }
                }
                #endregion

                #region OpcionAuxilioEducativo, CantidadHoraSemana, FechaInicioEstudio, FechaFinalizacionEstudio
                if (tipoBeneficio.PermiteAuxilioEducativo)
                {
                    if (OpcionAuxilioEducativo == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "OpcionAuxilioEducativo" }));
                    }
                }
                if (tipoBeneficio.PermiteAuxilioEducativo || tipoBeneficio.PermisoEstudio)
                {

                    if (FechaInicioEstudio == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "FechaInicioEstudio" }));
                    }
                    else if (FechaInicioEstudio < hoy)
                    {
                        errores.Add(new ValidationResult(
                            "La fecha de inicio estudio no debe ser menor que la fecha actual.",
                            new[] { "FechaInicioEstudio" }));
                    }

                    if (FechaFinalizacionEstudio == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "FechaFinalizacionEstudio" }));
                    }
                    else if (FechaFinalizacionEstudio < FechaInicioEstudio)
                    {
                        errores.Add(new ValidationResult(
                           "La fecha de finalización de estudio no debe ser menor a la fecha de inicio de estudio.",
                           new[] { "FechaFinalizacionEstudio" }));
                    }
                }
                if (tipoBeneficio.PermisoEstudio)
                {
                    if (CantidadHoraSemana == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "CantidadHoraSemana" }));
                    }
                }
                #endregion

                #region ValorAutorizado
                if (ValorAutorizado == null)
                {
                    if (ValorAutorizado < 1 || ValorAutorizado > 99999999)
                    {
                        errores.Add(new ValidationResult("Rango permitido de 1 -99.999.999", new[] { "ValorAutorizado" }));
                    }
                }

                #endregion

                #region BeneficiosSubperiodos
                if (BeneficiosSubperiodos != null)
                {
                    foreach (var item in BeneficiosSubperiodos)
                    {
                        var subperiodo = contexto.SubPeriodos.FirstOrDefault(x => x.Id == item.SubPeriodoId);
                        if (subperiodo == null)
                        {
                            errores.Add(new ValidationResult($"{item.SubPeriodoId} no es un subperíodo válido.",
                                new[] { "BeneficiosSubperiodos" }));
                        }
                        else
                        {
                            if (subperiodo.TipoPeriodoId != TipoPeriodoId)
                            {
                                errores.Add(new ValidationResult($"{item.SubPeriodoId} no es un subperíodo, del periodo {TipoPeriodoId}.",
                                new[] { "BeneficiosSubperiodos" }));
                            }
                        }

                    }
                }
                #endregion

                #region BeneficiosAdjuntos
                if (tipoBeneficio != null)
                {
                    if (BeneficiosAdjuntos != null)
                    {
                        foreach (var item in BeneficiosAdjuntos)
                        {
                            var subperiodo = contexto.TipoBeneficioRequisitos
                                                     .FirstOrDefault(x => x.Id == item.TipoBeneficioRequisitoId &&
                                                                          x.TipoBeneficioId == TipoBeneficioId);
                            if (subperiodo == null)
                            {
                                errores.Add(new ValidationResult("No existe", new[] { "beneficiosAdjuntos" }));
                            }
                        }
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
