using MediatR;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Reclutamiento.Dominio.Autenticacion.Registro
{
    public class RegistroAutenticacionRequest : IRequest<CommandResult>, IValidatableObject
    {
        
        #region DATOSBASICOS
        #region PrimerNombre
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                  ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string PrimerNombre { get; set; }
        #endregion

        #region SegundoNombre
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                  ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string SegundoNombre { get; set; }
        #endregion

        #region PrimerApellido
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                  ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string PrimerApellido { get; set; }
        #endregion

        #region SegundoApellido        
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                  ConstantesExpresionesRegulares.Espacio + "]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string SegundoApellido { get; set; }
        #endregion


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? SexoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(10, ErrorMessage = ConstantesErrores.Minimo + "10.")]
        [MaxLength(12, ErrorMessage = ConstantesErrores.Maximo + "12.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        public string Celular { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? TipoDocumentoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(15, ErrorMessage = ConstantesErrores.Maximo + "15.")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EmailAddress(ErrorMessage = ConstantesErrores.CorreoElectronico)]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string CorreoElectronicoPersonal { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (ReclutamientoDbContext)validationContext.GetService(typeof(ReclutamientoDbContext));
                DateTime fechaActual = DateTime.Today;

                #region DATOSBASICOS
                #region SexoId
                if (SexoId != null)
                {
                    var genero = contexto.Sexos.FirstOrDefault(x => x.Id == SexoId);
                    if (genero == null)
                    {
                        errores.Add(new ValidationResult("El sexo que intentas guardar no existe.", new[] { "SexoId" }));
                    }
                }
                #endregion
                #endregion

                #region IDENTIFICACION
                #region TipoDocumentoId
                if (TipoDocumentoId != null)
                {
                    var tipoDocumento = contexto.TipoDocumentos.FirstOrDefault(x => x.Id == TipoDocumentoId);
                    if (tipoDocumento == null)
                    {
                        errores.Add(new ValidationResult("El tipo de documento que intentas guardar no existe.",
                            new[] { "TipoDocumentoId" }));
                    }
                }
                #endregion

                #region NumeroDocumento
                var numeroDocumento = contexto.HojaDeVidas.FirstOrDefault(x => x.NumeroDocumento == NumeroDocumento);
                if (numeroDocumento != null)
                {
                    errores.Add(new ValidationResult("El número de documento que intentas guardar ya existe para un usuario.",
                        new[] { "NumeroDocumento" }));
                }
                #endregion

                #endregion

                #region OTROS
                
                #region CorreoElectronicoPersonal
                if (CorreoElectronicoPersonal != null)
                {
                    var correoElectronicoPersonal = contexto.HojaDeVidas
                    .FirstOrDefault(x => x.CorreoElectronicoPersonal == CorreoElectronicoPersonal);

                    if (correoElectronicoPersonal != null)
                    {
                        errores.Add(new ValidationResult("El correo electrónico personal que intentas guardar ya existe para un usuario.",
                            new[] { "CorreoElectronicoPersonal" }));
                    }
                }
                #endregion
                #region Celular
                if (Celular != null)
                {
                    var correoElectronicoPersonal = contexto.HojaDeVidas
                    .FirstOrDefault(x => x.Celular == Celular);

                    if (correoElectronicoPersonal != null)
                    {
                        errores.Add(new ValidationResult("El número de celular que ingresaste ya existe para un usuario.",
                            new[] { "Celular" }));
                    }
                }
                #endregion

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
