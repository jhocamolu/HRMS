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

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Parcial
{
    /// <summary>
    /// Clase encargada de realizar las validaicones para actualizaciones parciales de ConceptosNomina
    /// </summary>
    public class ParcialConceptoNominaRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(12, ErrorMessage = ConstantesErrores.Maximo + "12.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico + "]*$",
                                   ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }


        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Alias { get; set; }


        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(255, ErrorMessage = ConstantesErrores.Maximo + "255.")]
        public string Nombre { get; set; }

        [EnumDataType(typeof(TipoConceptoNomina), ErrorMessage = "No es un tipo de concepto de nómina válido.")]
        public TipoConceptoNomina? TipoConceptoNomina { get; set; }

        [EnumDataType(typeof(ClaseConceptoNomina), ErrorMessage = "No es una clase de concepto de nómina válida.")]
        public ClaseConceptoNomina? ClaseConceptoNomina { get; set; }


        [Range(minimum: 1, maximum: 500, ErrorMessage = ConstantesErrores.Rango + "1 a 500.")]
        public int? Orden { get; set; }


        public OrigenCentroCostoNomina? OrigenCentroCosto { get; set; }


        public OrigenTerceroNomina? OrigenTercero { get; set; }

        public bool? VisibleImpresion { get; set; }

        [EnumDataType(typeof(UnidadMedida), ErrorMessage = "No es una unidad de medida válida.")]
        public UnidadMedida? UnidadMedida { get; set; }

        public bool? RequiereCantidad { get; set; }

        public int? FuncionNominaId { get; set; }

        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 1, maximum: 9999999999, ErrorMessage = ConstantesErrores.Rango + "1 a 9999999999.")]
        public string NitTercero { get; set; }


        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Numerico + "]*$", ErrorMessage = ConstantesErrores.Numerico)]
        [Range(minimum: 0, maximum: 9, ErrorMessage = ConstantesErrores.Rango + "0 a 9.")]
        public int? DigitoVerificacion { get; set; }


        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500.")]
        public string Descripcion { get; set; }

        public int? TipoAdministradoraId { get; set; }

        #region Estado Registro
        public bool? Activo { get; set; }
        #endregion
        #endregion

        #region Validacion Manueales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Alias
                var orden = contexto.ConceptoNominas.FirstOrDefault(x => x.Id != Id && x.Alias == Alias);
                if (orden != null)
                {
                    errores.Add(new ValidationResult("El alias ya existe.",
                        new[] { "Alias" }));
                }
                #endregion

                #region Orden
                var alias = contexto.ConceptoNominas
                            .FirstOrDefault(x => x.Orden == Orden && x.ClaseConceptoNomina == ClaseConceptoNomina);
                if (alias != null)
                {
                    errores.Add(new ValidationResult("El orden que ingresaste ya está asignado para otro concepto para la clase de concepto.",
                        new[] { "Orden" }));
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

                #region OrigenCentroCosto
                if (OrigenCentroCosto.ToString() == "especifico")
                {
                    var cuentaCentrocosto = contexto.ConceptoNominaCuentaContables
                                                    .Where(x => x.ConceptoNominaId == Id)
                                                    .Count();
                    if (cuentaCentrocosto > 1)
                    {
                        errores.Add(new ValidationResult("El origen de centro de costo específico sólo permite tener un solo centro de costo configurado para el concepto, por favor revise.",
                       new[] { "OrigenCentroCosto" }));
                    }
                }
                #endregion

                #region OrigenTercero
                if (OrigenTercero.ToString() == "especifico")
                {
                    if (NitTercero == null)
                    {
                        errores.Add(new ValidationResult("Requerido.",
                      new[] { "NitTercero" }));
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
