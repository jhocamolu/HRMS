using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Reportes.Comandos.ArchivoTipo2Pilas
{
    public class ReporteArchivoTipo2PilaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? PeriodoPagoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoPlanillaId { get; set; }

        public int? NumeroPlanilla { get; set; }

        public DateTime? FechaPagoPlanilla { get; set; }

        public int? TipoCotizanteId { get; set; }

        public string SubtipoCotizante { get; set; }

        public string Funcionario { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var validaPeriodoContable = contexto.PeriodoContables.FirstOrDefault(x => x.Id == PeriodoPagoId);
                if (validaPeriodoContable == null)
                {
                    errores.Add(new ValidationResult(
                            $"{ConstantesErrores.NoExiste("el periodo contable")}",
                            new[] { "PeriodoPagoId" }));
                }

                var validaTipoPlanilla = contexto.TipoPlanillas.FirstOrDefault(x => x.Id == TipoPlanillaId);
                if (validaTipoPlanilla == null)
                {
                    errores.Add(new ValidationResult(
                            $"{ConstantesErrores.NoExiste("el periodo contable")}",
                            new[] { "TipoPlanillaId" }));
                }

                if (validaTipoPlanilla.Codigo == "N")
                {
                    if (NumeroPlanilla == null)
                    {
                        errores.Add(new ValidationResult(
                                $"{ConstantesErrores.Requerido}",
                                new[] { "NumeroPlanilla" }));
                    }
                    if (FechaPagoPlanilla == null)
                    {
                        errores.Add(new ValidationResult(
                                $"{ConstantesErrores.Requerido}",
                                new[] { "FechaPagoPlanilla" }));
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
