using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Reportes.Comandos.BeneficiosCorporativos
{
    public class ReporteBeneficioCorporativoRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicial { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaFinal { get; set; }

        public string TipoBeneficio { get; set; }

        public string EstadoFuncionario { get; set; }

        public string CentroOperativo { get; set; }

        public string Dependencia { get; set; }

        public string Cargo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                if (!String.IsNullOrEmpty(CentroOperativo))
                {
                    var splitCentroOperativo = CentroOperativo.Split(',');
                    foreach (var item in splitCentroOperativo)
                    {
                        var validaCentroOperativo = contexto.CentroOperativos.FirstOrDefault(c => c.Id == int.Parse(item));
                        if (validaCentroOperativo == null)
                        {
                            errores.Add(new ValidationResult(
                                $"No existe uno de los centros operativos que esta seleccionando.",
                                new[] { "CentroOperativo" }));
                        }
                    }
                }
                if (!String.IsNullOrEmpty(Dependencia))
                {
                    var splitDependencia = Dependencia.Split(',');
                    foreach (var item in splitDependencia)
                    {
                        var validaDependencia = contexto.Dependencias.FirstOrDefault(c => c.Id == int.Parse(item));
                        if (validaDependencia == null)
                        {
                            errores.Add(new ValidationResult(
                                $"No existe una de las dependencia que esta seleccionando.",
                                new[] { "Dependencia" }));
                        }
                    }
                }
                if (!String.IsNullOrEmpty(Cargo))
                {
                    var splitCargo = Cargo.Split(',');
                    foreach (var item in splitCargo)
                    {
                        var validaCargo = contexto.Cargos.FirstOrDefault(c => c.Id == int.Parse(item));
                        if (validaCargo == null)
                        {
                            errores.Add(new ValidationResult(
                                $"No existe uno de los cargos que esta seleccionando.",
                                new[] { "Cargo" }));
                        }
                    }
                }

                if (!String.IsNullOrEmpty(TipoBeneficio))
                {
                    var splitTipoBeneficio = TipoBeneficio.Split(',');
                    foreach (var item in splitTipoBeneficio)
                    {
                        var validaTipoBeneficio = contexto.TipoBeneficios.FirstOrDefault(c => c.Id == int.Parse(item));
                        if (validaTipoBeneficio == null)
                        {
                            errores.Add(new ValidationResult(
                                $"No existe uno de los tipos de beneficio que esta seleccionando.",
                                new[] { "TipoBeneficio" }));
                        }
                    }
                }

                var fechaActual = DateTime.Now.Date;

                if (FechaInicial != null && FechaFinal != null)
                {
                    var fechaI = (DateTime)FechaInicial;
                    var fechaF = (DateTime)FechaFinal;
                    if (fechaI.Date == fechaActual)
                    {
                        errores.Add(new ValidationResult("La fecha inicial no puede ser igual a la fecha actual.",
                                    new[] { "FechaInicial" }));
                    }
                    if (fechaI.Date > fechaActual)
                    {
                        errores.Add(new ValidationResult("La fecha inicial no puede ser posterior a la fecha actual.",
                                    new[] { "FechaInicial" }));
                    }
                    if (fechaF.Date < fechaI.Date)
                    {
                        errores.Add(new ValidationResult("La fecha final no puede ser menor a la fecha inicial.",
                                    new[] { "FechaFinal" }));
                    }
                    if (fechaF.Date == fechaI.Date)
                    {
                        errores.Add(new ValidationResult("La fecha final no puede ser igual a la fecha inicial.",
                                    new[] { "FechaFinal" }));
                    }
                    if (fechaF.Date > fechaActual)
                    {
                        errores.Add(new ValidationResult("La fecha final no puede ser posterior a la fecha actual.",
                                    new[] { "FechaFinal" }));
                    }
                }
            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
    }
}
