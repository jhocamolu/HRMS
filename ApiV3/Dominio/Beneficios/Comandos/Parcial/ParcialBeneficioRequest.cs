using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace ApiV3.Dominio.Beneficios.Comandos.Parcial
{
    public class BeneficiosSubperiodos
    {
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
    public class ParcialBeneficioRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        #region Elementos
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? FuncionarioId { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoBeneficioId { get; set; }


        public DateTime? FechaSolicitud { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public double? ValorSolicitud { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? PlazoMaximo { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? TipoPeriodoId { get; set; }


        [EnumDataType(typeof(OpcionAuxilioEducativo), ErrorMessage = ConstantesErrores.Emun)]
        public OpcionAuxilioEducativo? OpcionAuxilioEducativo { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public int? CantidadHoraSemana { get; set; }


        public DateTime? FechaInicioEstudio { get; set; }


        public DateTime? FechaFinalizacionEstudio { get; set; }


        [MaxLength(300, ErrorMessage = ConstantesErrores.Maximo + ("300."))]
        public string Observacion { get; set; }


        [EnumDataType(typeof(EstadoBeneficiosCorportativos), ErrorMessage = ConstantesErrores.Emun)]
        public EstadoBeneficiosCorportativos? Estado { get; set; }



        public string ObservacionAprobacion { get; set; }



        public string ObservacionAutorizacion { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public double? ValorAutorizado { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public double? ValorCobrar { get; set; }

        [Range(0.0, 5.0, ErrorMessage = ConstantesErrores.Rango + "0 al 5.")]
        public float? NotaAcademica { get; set; }


        public string ObservacionNotaAcademica { get; set; }

        #endregion

        #region Lista
        public List<BeneficiosSubperiodos> BeneficiosSubperiodos { get; set; } = new List<BeneficiosSubperiodos>();

        public List<BeneficiosAdjuntos> BeneficiosAdjuntos { get; set; } = new List<BeneficiosAdjuntos>();
        #endregion
        #region Activo
        public bool? Activo { get; set; }
        #endregion
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            var hoy = DateTime.Today;
            Beneficio existeId = null;
            TipoBeneficio tipoBeneficio = null;
            double diasLaborados = 0;
            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                #region Id
                if (Id != null)
                {
                    existeId = contexto.Beneficios.FirstOrDefault(x => x.Id == Id);
                    if (existeId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("beneficio"),
                            new[] { "Id" }));
                        return errores;
                    }
                    else
                    {
                        tipoBeneficio = contexto.TipoBeneficios.FirstOrDefault(x => x.Id == existeId.TipoBeneficioId);
                    }
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
                if (TipoBeneficioId != null)
                {
                    tipoBeneficio = contexto.TipoBeneficios.FirstOrDefault(x => x.Id == TipoBeneficioId);
                    if (tipoBeneficio == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo beneficio"), new[] { "TipoBeneficioId" }));
                    }
                    else if (diasLaborados != 0 && tipoBeneficio.DiasAntiguedad > diasLaborados)
                    {
                        errores.Add(new ValidationResult(
                            "El funcionario que intentas ingresar no cumple con la condición de antigüedad para acceder al beneficio.",
                            new[] { "FuncionarioId" }));
                    }
                }
                else
                {
                }
                #endregion

                #region ValorSolicitud
                if (tipoBeneficio != null && tipoBeneficio.ValorSolicitado)
                {
                    if (ValorSolicitud != null)
                    {
                        if (ValorSolicitud > tipoBeneficio.MontoMaximo)
                        {
                            errores.Add(new ValidationResult(
                            $"El valor que solicitas no debe ser mayor a ${tipoBeneficio.MontoMaximo.ToString("0,0", elGR)}",
                            new[] { "ValorSolicitud" }));
                        }
                    }
                }
                #endregion

                #region PlazoMaximo
                if (tipoBeneficio != null && tipoBeneficio.PlazoMes)
                {
                    if (PlazoMaximo == null)
                    {
                        if (PlazoMaximo > tipoBeneficio.CuotaPermitida)
                        {
                            errores.Add(new ValidationResult(
                                "El plazo máximo en meses que ingresaste no debe ser mayor al establecido para el beneficio.",
                                new[] { "PlazoMaximo" }));
                        }
                    }
                }
                #endregion

                #region TipoPeriodoId
                if (tipoBeneficio != null && tipoBeneficio.PeriodoPago)
                {
                    if (TipoPeriodoId != null)
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
                if (tipoBeneficio != null && tipoBeneficio.PermiteAuxilioEducativo)
                {
                    var dateTime = (DateTime)existeId.FechaCreacion;
                    DateTime fechaCreacion = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

                    if (FechaInicioEstudio != null || FechaFinalizacionEstudio != null)
                    {
                        if (FechaInicioEstudio != null && FechaFinalizacionEstudio == null)
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "FechaFinalizacionEstudio" }));
                        }
                        else if (FechaInicioEstudio == null && FechaFinalizacionEstudio != null)
                        {
                            errores.Add(new ValidationResult(ConstantesErrores.Requerido, new[] { "FechaInicioEstudio" }));
                        }
                        else if (DateTime.Compare((DateTime)FechaInicioEstudio, fechaCreacion) < 0)
                        {
                            errores.Add(new ValidationResult(
                                "La fecha de inicio estudio no debe ser menor que la solicitud.",
                                new[] { "FechaInicioEstudio" }));
                        }
                        else if (FechaFinalizacionEstudio < FechaInicioEstudio)
                        {
                            errores.Add(new ValidationResult(
                               "La fecha de finalización de estudio no debe ser menor a la fecha de inicio de estudio.",
                               new[] { "FechaFinalizacionEstudio" }));
                        }
                    }
                }
                #endregion

                #region ValorAutorizado
                if (ValorAutorizado != null)
                {
                    if (ValorAutorizado < 1 || ValorAutorizado > 99999999)
                    {
                        errores.Add(new ValidationResult("Rango permitido de 1 -99.999.999", new[] { "ValorAutorizado" }));
                    }
                    else if (tipoBeneficio.MontoMaximo > 0 && ValorAutorizado > tipoBeneficio.MontoMaximo)
                    {

                        errores.Add(new ValidationResult($"El valor autorizado no debe ser mayor a ${tipoBeneficio.MontoMaximo.ToString("0,0", elGR)}",
                            new[] { "ValorAutorizado" }));
                    }
                    else
                    {
                        if (tipoBeneficio.PlazoMes)
                        {
                            var cuotas = contexto.BeneficioSubperiodos.Where(x => x.BeneficioId == existeId.Id).Count();
                            ValorCobrar = ValorAutorizado / (cuotas * existeId.PlazoMaximo);

                        }

                    }
                }

                #endregion

                #region BeneficiosSubperiodos
                if (BeneficiosSubperiodos != null)
                {
                    if (tipoBeneficio != null)
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
