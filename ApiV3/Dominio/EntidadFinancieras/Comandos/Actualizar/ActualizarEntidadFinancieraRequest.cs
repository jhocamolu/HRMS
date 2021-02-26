using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.EntidadesFinancieras.Comandos.Actualizar
{
    public class ActualizarEntidadFinancieraRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1, 999, ErrorMessage = ConstantesErrores.Rango + "1 - 999.")]
        [RegularExpression(@"[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + " 15.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Nit { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MaxLength(1, ErrorMessage = ConstantesErrores.Maximo + " 1.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Dv { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(80, ErrorMessage = ConstantesErrores.Maximo + " 80.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? DivisionPoliticaNivel2Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(1111111, 9999999999, ErrorMessage = ConstantesErrores.Rango + "1111111 - 9999999999.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]+$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(80, ErrorMessage = ConstantesErrores.Maximo + " 80.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]+$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string RepresentanteLegal { get; set; }

        public bool? EntidadPorDefecto { get; set; }
        #endregion
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                //Elemento no existe
                var existe = dbContexto.EntidadFinancieras.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe",
                        new[] { "Id" }));
                    return errores;
                }

                #region Código
                //Valida que código sea único
                var elemento = dbContexto.EntidadFinancieras.FirstOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                if (elemento != null)
                {
                    errores.Add(new ValidationResult(
                        $"El código que intentas guardar esta asociado a una entidad financiera existente.",
                        new[] { "Codigo" }));
                }
                #endregion
                #region Nit
                //Valida que Nit sea único
                var validaNit = dbContexto.EntidadFinancieras.FirstOrDefault(x => x.Nit == Nit && x.Id != Id);
                if (validaNit != null)
                {
                    errores.Add(new ValidationResult(
                        $"El NIT que intentas guardar esta asociado a una entidad financiera existente.",
                        new[] { "Nit" }));
                }
                #endregion
                #region Nombre
                //Valida nombre sea único
                var validaNombre = dbContexto.EntidadFinancieras.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                if (validaNombre != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre que intentas guardar esta asociado a una entidad financiera existente.",
                        new[] { "Nombre" }
                        ));
                }
                #endregion
                #region DigitoVerificacion

                if (Dv != DigitoVerificacion.CalcularDigitoVerificacion(Nit))
                {
                    errores.Add(new ValidationResult(
                        $"El digito de verificación no corresponde al NIT registrado : {Dv} , Sugerido {DigitoVerificacion.CalcularDigitoVerificacion(Nit)} ",
                        new[] { "Dv" }));
                }
                #endregion
                #region Division Política
                //Valida División Política nivel 2 exista
                if (DivisionPoliticaNivel2Id != null)
                {
                    var divisionPoliticaNivel2 = dbContexto.DivisionPoliticaNiveles2.FirstOrDefault(x => x.Id == DivisionPoliticaNivel2Id);
                    if (divisionPoliticaNivel2 == null)
                    {
                        errores.Add(new ValidationResult(
                            $"No División política nivel 2",
                            new[] { "DivisionPoliticaNivel2Id" }
                            ));
                    }
                }
                #endregion
                #region EntidadPorDefecto
                if (EntidadPorDefecto == true)
                {
                    var existeEntidadPorDefecto = dbContexto.EntidadFinancieras
                                                 .FirstOrDefault(x => x.EntidadPorDefecto == true &&
                                                                      x.Id != Id);
                    if (existeEntidadPorDefecto != null)
                    {
                        errores.Add(new ValidationResult(
                          "Ya existe una entidad financiera por defecto.",
                          new[] { "EntidadPorDefecto" }
                          ));
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
    }
}
