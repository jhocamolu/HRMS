using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiV3.Dominio.ParametroGenerales.Comandos.Parcial
{
    public class ParcialParametroGeneralRequest : IRequest<CommandResult>, IValidatableObject
    {

        #region Validaciones

        public string Valor { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Alias { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int AnnoVigenciaId { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();

            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region id
                var elemento = dbContexto.ParametroGenerales.SingleOrDefault(x => x.Alias == Alias &&
                                                                                  x.AnnoVigenciaId == AnnoVigenciaId);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe",
                       new[] { elemento.Alias }));
                }
                if (elemento.Obligatorio == true && String.IsNullOrEmpty(Valor))
                {
                    errores.Add(new ValidationResult(
                       $"Requerido",
                       new[] { elemento.Alias }));
                }
                if (!String.IsNullOrEmpty(Valor))
                {
                    Regex rgx = new Regex(@"[*]+");
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
                        case TipoDato.TextArea:
                            rgx = new Regex(@"^[*]+$");
                            break;
                        case TipoDato.Url:
                            rgx = new Regex(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                ConstantesExpresionesRegulares.Alfanumerico + " ]+$");
                            break;
                        case TipoDato.Email:
                            rgx = new Regex(@"^[*]+$");
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
                        if (!DateTime.TryParse(Valor, out temp))
                        {
                            errores.Add(new ValidationResult(
                             $"Formato no compatible.",
                             new[] { elemento.Alias }));

                        }
                    }
                    if (elemento.Tipo == TipoDato.Time)
                    {
                        TimeSpan temp;
                        if (!TimeSpan.TryParse(Valor, out temp))
                        {
                            errores.Add(new ValidationResult(
                             $"Formato no compatible.",
                             new[] { elemento.Alias }));

                        }
                    }
                    if (elemento.Tipo != TipoDato.Datetime && elemento.Tipo != TipoDato.Date && elemento.Tipo != TipoDato.Time)
                    {
                        if (!rgx.IsMatch(Valor))
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

            return errores;
        }
        #endregion
    }
}
