using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Reportes.Comandos.MaestroFuncionarios
{
    public class ReporteMaestroFuncionarioRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
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

            }
            catch (Exception e)
            {
                errores.Add(new ValidationResult(e.Message));
            }
            return errores;
        }
    }
}
