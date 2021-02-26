using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Reportes.Comandos.NovedadLibranza
{
    public class ReporteNovedadLibranzaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(MetodoReporteEmbargo), ErrorMessage = "No es un método valido.")]
        public MetodoReporteEmbargo Metodo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoLiquidacionId { get; set; }

        public string SubperiodoId { get; set; }

        public int? NominaAnio { get; set; }

        public string NominaMes { get; set; }

        public DateTime? FechaInicial { get; set; }

        public DateTime? FechaFinal { get; set; }

        #endregion
        #region ValidacionManual

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region TipoLiquidacionId
                if (TipoLiquidacionId != null)
                {
                    var validaTipoLiquidacionId = dbContexto.TipoLiquidaciones.FirstOrDefault(x => x.Id == TipoLiquidacionId);
                    if (validaTipoLiquidacionId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("tipo liquidación"),
                           new[] { "FuncionarioId" }));
                    }
                }
                #endregion
                #region Método por período
                if (Metodo == MetodoReporteEmbargo.PorPeriodo)
                {
                    var anioActual = DateTime.Now.Year;
                    var mesActual = DateTime.Now.Month;
                    bool banderaMesActual = false;
                    bool banderaTodosMeses = false;


                    if (NominaAnio != null)
                    {
                        if (NominaAnio > anioActual)
                        {
                            errores.Add(new ValidationResult("No se puede generar un reporte de un año posterior al año actual.",
                               new[] { "NominaAnio" }));
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(NominaMes))
                            {
                                string[] meses = NominaMes.Split(",");
                                var todosLosmeses = false;
                                foreach (var item in meses)
                                {
                                    if (Convert.ToInt32(item) > mesActual &&
                                        Convert.ToInt32(item) != 0 &&
                                        NominaAnio == anioActual)
                                    {
                                        banderaMesActual = true;
                                    }
                                    if (Convert.ToInt32(item) == 0)
                                    {
                                        if (NominaAnio == anioActual)
                                        {
                                            banderaTodosMeses = true;
                                        }
                                        else
                                        {
                                            todosLosmeses = true;
                                        }
                                    }
                                }
                                if (banderaMesActual == true)
                                {
                                    errores.Add(new ValidationResult("No se puede generar un reporte de un mes posterior al mes actual, ya que el año que ingresaste, corresponde al año actual.",
                                        new[] { "snack" }));
                                }
                                else if (banderaTodosMeses == true)
                                {
                                    errores.Add(new ValidationResult("No se puede generar un reporte de todos los meses, ya que el año que ingresaste, corresponde al año actual.",
                                        new[] { "snack" }));
                                }
                                if (todosLosmeses == true)
                                {
                                    NominaMes = "1,2,3,4,5,6,7,8,9,10,11,12";
                                }
                            }
                            else
                            {
                                errores.Add(new ValidationResult("Requerido",
                                    new[] { "NominaMes" }));
                            }
                        }
                    }
                    else
                    {
                        errores.Add(new ValidationResult("Requerido",
                           new[] { "NominaAnio" }));
                    }
                    if (SubperiodoId == null)
                    {
                        errores.Add(new ValidationResult("Requerido",
                           new[] { "SubperiodoId" }));
                    }
                }
                var fechaActual = DateTime.Now.Date;

                if (Metodo == MetodoReporteEmbargo.Acumulado)
                {
                    if (FechaInicial != null && FechaFinal != null)
                    {
                        var fechaI = (DateTime)FechaInicial;
                        var fechaF = (DateTime)FechaFinal;
                        if (fechaI.Date == fechaActual)
                        {
                            errores.Add(new ValidationResult("La fecha inicial no puede ser igual a la fecha actual.",
                                        new[] { "FechaInicial" }));
                        }
                        if (fechaI > fechaActual)
                        {
                            errores.Add(new ValidationResult("La fecha inicial no puede ser posterior a la fecha actual.",
                                        new[] { "FechaInicial" }));
                        }
                        if (fechaF < fechaI)
                        {
                            errores.Add(new ValidationResult("La fecha final no puede ser menor a la fecha inicial.",
                                        new[] { "FechaFinal" }));
                        }
                        if (fechaF == fechaI)
                        {
                            errores.Add(new ValidationResult("La fecha final no puede ser igual a la fecha inicial.",
                                        new[] { "FechaFinal" }));
                        }
                        if (fechaF > fechaActual)
                        {
                            errores.Add(new ValidationResult("La fecha final no puede ser posterior a la fecha actual.",
                                        new[] { "FechaFinal" }));
                        }
                    }
                    else
                    {
                        if (FechaInicial == null)
                        {
                            errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaInicial" }));
                        }
                        if (FechaFinal == null)
                        {
                            errores.Add(new ValidationResult("Requerido",
                            new[] { "FechaFinal" }));
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
