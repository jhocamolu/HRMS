using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.RangoUvts.Comandos.Actualizar
{
    public class ActualizarRangoUvtRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(0, 99999, ErrorMessage = ConstantesErrores.Rango + "0 - 99999.")]
        public int? Desde { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(0, 99999, ErrorMessage = ConstantesErrores.Rango + "0 - 99999.")]
        public int? Hasta { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(0, 100, ErrorMessage = ConstantesErrores.Rango + "0 - 100.")]
        public decimal? Porcentaje { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(0, 10000, ErrorMessage = ConstantesErrores.Rango + "0 - 10000.")]
        public int? Adiciona { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(0, 10000, ErrorMessage = ConstantesErrores.Rango + "0 - 10000.")]
        public int? Sustrae { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? ValidoDesde { get; set; }

        #endregion
        #region ValidacionManual
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                var existe = dbContexto.RangoUvts.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("rango UVT"),
                           new[] { "Id" }));
                }


                #region Rango Existe
                var validaExiste = dbContexto.RangoUvts.FirstOrDefault(x => x.Id != Id &&
                                                                            (x.Desde == Desde &&
                                                                            x.Hasta == Hasta && x.ValidoDesde == ValidoDesde &&
                                                                            x.ValidoDesde == ValidoDesde));
                if (validaExiste != null)
                {
                    errores.Add(new ValidationResult("El rango UVT que intentas guardar ya existe.",
                           new[] { "snackError" }));
                }
                else
                {
                    var banderaMensaje = false;

                    var validaRangoHastaCero = dbContexto.RangoUvts.FirstOrDefault(x => x.Id != Id &&
                                                                                        x.ValidoDesde == ValidoDesde &&
                                                                                        (x.Hasta == 0 || x.Hasta == null) &&
                                                                                        ((x.Desde <= Desde) || (x.Desde <= Hasta))
                                                                                         );
                    if (validaRangoHastaCero != null)
                    {
                        banderaMensaje = true;
                    }
                    else
                    {
                        var validaDentroDeRango = dbContexto.RangoUvts.FirstOrDefault(x => x.Id != Id && x.ValidoDesde == ValidoDesde &&
                                                                                    ((x.Desde <= Desde && x.Hasta >= Desde) ||
                                                                                    (x.Desde <= Hasta && x.Hasta >= Hasta)));
                        if (validaDentroDeRango != null)
                        {
                            banderaMensaje = true;
                        }
                    }
                    if (banderaMensaje == true)
                    {
                        banderaMensaje = true;
                        errores.Add(new ValidationResult("El rango UVT que intentas guardar se encuentra dentro de otro rango.",
                               new[] { "snackError" }));
                    }
                }
                #endregion
                if (Hasta != 0)
                {
                    if (Hasta < Desde)
                    {
                        errores.Add(new ValidationResult("El valor  que intentas guardar no puede ser menor que el valor desde.",
                                       new[] { "Hasta" }));
                    }
                }

                if ((DateTime)ValidoDesde.Value.Date != existe.ValidoDesde.Date && (DateTime)ValidoDesde.Value.Date < DateTime.Now.Date)
                {
                    errores.Add(new ValidationResult("La fecha de validez que intentas guardar no debe ser menor que la fecha actual.",
                                       new[] { "ValidoDesde" }));
                }
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
