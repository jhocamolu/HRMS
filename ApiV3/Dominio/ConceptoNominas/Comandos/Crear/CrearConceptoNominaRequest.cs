using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static ApiV3.Infraestructura.Utilidades.DigitoVerificacion;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Crear
{
    /// <summary>
    /// Clase encargada de realizar las validaicones para crear nuevo registros en ConceptosNomina
    /// </summary>
    public class CrearConceptoNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(12, ErrorMessage = ConstantesErrores.Maximo + "12.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico + "]*$",
                                   ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Alias { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(TipoConceptoNomina), ErrorMessage = "No es un tipo de concepto de nómina válido.")]
        public TipoConceptoNomina? TipoConceptoNomina { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(ClaseConceptoNomina), ErrorMessage = "No es una clase de concepto de nómina válida.")]
        public ClaseConceptoNomina? ClaseConceptoNomina { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ConceptoAgrupador { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public OrigenCentroCostoNomina OrigenCentroCosto { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public OrigenTerceroNomina OrigenTercero { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? VisibleImpresion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(UnidadMedida), ErrorMessage = "No es una unidad de medida válida.")]
        public UnidadMedida UnidadMedida { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? RequiereCantidad { get; set; }

        public int? FuncionNominaId { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 1, maximum: 9999999999, ErrorMessage = ConstantesErrores.Rango + "1 a 9999999999.")]
        public string NitTercero { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 0, maximum: 9, ErrorMessage = ConstantesErrores.Rango + "0 a 9.")]
        public int? DigitoVerificacion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Descripcion { get; set; }

        public List<int> Bases { get; set; }

        public List<int> Agrupadores { get; set; }

        public int? TipoAdministradoraId { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));

                #region Alias
                var alias = contexto.ConceptoNominas.FirstOrDefault(x => x.Alias == Alias);
                if (alias != null)
                {
                    errores.Add(new ValidationResult("El alias ya existe.",
                        new[] { "Alias" }));
                }
                #endregion

                #region Bases & Agrupadores
                if (ConceptoAgrupador != null)
                {
                    // si envia falso debe enviar los agrupadores
                    if (ConceptoAgrupador == false && Agrupadores != null)
                    {

                        if (Bases != null)
                        {
                            errores.Add(new ValidationResult("No se deben ingresar conceptos no agrupadores.",
                                            new[] { "Agrupadores" }));
                        }
                        foreach (var item in Agrupadores)
                        {
                            var validaAgrupador = contexto.ConceptoNominas.Find(item);
                            if (validaAgrupador == null)
                            {
                                errores.Add(new ValidationResult("El concepto agrupador no existe.",
                                            new[] { "Agrupadores" }));
                            }
                            else if (validaAgrupador.ConceptoAgrupador != true)
                            {
                                errores.Add(new ValidationResult("El concepto agrupador que intentas ingresar no es valido.",
                                           new[] { "Agrupadores" }));
                            }
                        }
                    }
                    // si envia verdadero debe enviar las bases para generar el agrupador
                    if (ConceptoAgrupador == true && Bases != null)
                    {
                        if (Agrupadores != null)
                        {
                            errores.Add(new ValidationResult("No se deben ingresar conceptos agrupadores.",
                                            new[] { "Agrupadores" }));
                        }
                        foreach (var item in Bases)
                        {
                            var validaBase = contexto.ConceptoNominas.Find(item);
                            if (validaBase == null)
                            {
                                errores.Add(new ValidationResult("El concepto base no existe.",
                                            new[] { "Bases" }));
                            }
                            else if (validaBase.ConceptoAgrupador != false)
                            {
                                errores.Add(new ValidationResult("El concepto base que intentas ingresar no es valido.",
                                           new[] { "Bases" }));
                            }
                        }
                    }
                }
                #endregion

                #region Requiere Cantidad
                if (RequiereCantidad == true)
                {
                    if (FuncionNominaId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                        new[] { "FuncionNominaId" }));
                    }
                    else
                    {
                        var funcion = contexto.FuncionNominas.FirstOrDefault(x => x.Id == FuncionNominaId);
                        if (funcion == null)
                        {
                            errores.Add(new ValidationResult("La función que intentas ingresar no existe.",
                            new[] { "FuncionNominaId" }));
                        }
                    }
                }
                #endregion

                #region DigitoVerificacion
                if (NitTercero != null)
                {
                    var digito = CalcularDigitoVerificacion(NitTercero);
                    if (digito != DigitoVerificacion.ToString())
                    {
                        errores.Add(new ValidationResult("El número que ingresaste es incorrecto, por favor verifica el DV.",
                        new[] { "DigitoVerificacion" }));
                    }
                }

                #endregion

                #region Tipo Administradora
                if (TipoAdministradoraId != null)
                {
                    var tipoAdministradora = contexto.TipoAdministradoras.FirstOrDefault(x => x.Id == TipoAdministradoraId);
                    if (tipoAdministradora == null)
                    {
                        errores.Add(new ValidationResult("El tipo administradora no existe.",
                            new[] { "TipoAdministradoraId" }));
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
