using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiV3.Dominio.ParametroGenerales.Comandos.ParcialGrupo
{
    public class Valores
    {
        public string Valor { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Alias { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int AnnoVigenciaId { get; set; }
    }

    public class ParcialGrupoParametroGeneralRequest : IRequest<CommandResult>, IValidatableObject
    {
        public List<Valores> valores { get; set; } = new List<Valores>();

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
            foreach (var item in valores)
            {
                try
                {
                    #region id
                    var elemento = dbContexto.ParametroGenerales.FirstOrDefault(x => x.Alias == item.Alias &&
                                                                                  x.AnnoVigenciaId == item.AnnoVigenciaId);
                    if (elemento == null)
                    {
                        errores.Add(new ValidationResult(
                            $"No Existe",
                            new[] { elemento.Alias }));
                    }
                    if (elemento.Obligatorio == true && String.IsNullOrEmpty(item.Valor))
                    {
                        errores.Add(new ValidationResult(
                           $"Requerido",
                           new[] { elemento.Alias }));
                    }
                    if (!String.IsNullOrEmpty(item.Valor))
                    {
                        Regex rgx = new Regex(@".*\s*");
                        switch (elemento.Tipo)
                        {
                            case TipoDato.Text:
                                rgx = new Regex(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                    ConstantesExpresionesRegulares.Alfanumerico + " ]+$");
                                break;
                            case TipoDato.Number:
                                rgx = new Regex(@"^[" + ConstantesExpresionesRegulares.Numerico + " ]+$");
                                break;
                            case TipoDato.Select:
                                rgx = new Regex(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + " ]+$");
                                break;
                            case TipoDato.Url:
                                rgx = new Regex(@"^[(?:http(s)?:\/\/)?[\w\.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]]*$");
                                break;
                            case TipoDato.Email:
                                rgx = new Regex(@"[\w\._-]{1,30}\+?[\w]{0,10}@[\w\.\-]{3,}\.\w{2,5}");

                                break;
                            case TipoDato.Tel:
                                rgx = new Regex(@"^[" + ConstantesExpresionesRegulares.Numerico + " ]+$");
                                break;
                            default:
                                break;
                        }

                        if (elemento.Tipo == TipoDato.Datetime || elemento.Tipo == TipoDato.Date)
                        {
                            DateTime temp;
                            if (!DateTime.TryParse(item.Valor, out temp))
                            {
                                errores.Add(new ValidationResult(
                                    $"Formato no compatible.",
                                    new[] { elemento.Alias }));

                            }
                        }
                        if (elemento.Tipo == TipoDato.Time)
                        {
                            TimeSpan temp;
                            if (!TimeSpan.TryParse(item.Valor, out temp))
                            {
                                errores.Add(new ValidationResult(
                                    $"Formato no compatible.",
                                    new[] { elemento.Alias }));

                            }
                        }
                        if (elemento.Tipo != TipoDato.Datetime && elemento.Tipo != TipoDato.Date && elemento.Tipo != TipoDato.Time)
                        {
                            if (!rgx.IsMatch(item.Valor))
                            {
                                errores.Add(new ValidationResult(
                                    $"Formato no compatible.",
                                    new[] { elemento.Alias }));
                            }
                        }
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    errores.Add(new ValidationResult(e.Message));
                }
            }
            return errores;
        }
        #endregion
    }
}
