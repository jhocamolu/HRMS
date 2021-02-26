using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.CategoriaNovedades.Comandos.Actualizar
{
    public class ActualizarCategoriaNovedadRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(60, ErrorMessage = ConstantesErrores.Maximo + " 60.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico + " ]*$", ErrorMessage = ConstantesErrores.Alfabetico)]
        public string Nombre { get; set; }

        public int? ConceptoNominaId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public ModuloSistema? Modulo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public ClaseCategoriaNovedad? Clase { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? UsaParametrizacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? RequiereTercero { get; set; }

        public UbicacionTerceroCategoriaNovedad? UbicacionTercero { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? ValorEditable { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                if (ConceptoNominaId != null)
                {
                    var existeConcepto = dbContexto.ConceptoNominas.FirstOrDefault(x => x.Id == ConceptoNominaId && x.EstadoRegistro == EstadoRegistro.Activo);
                    if (existeConcepto == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("concepto de nómina"),
                           new[] { "ConceptoNominaId" }));
                    }
                }

                var nombreExiste = dbContexto.CategoriaNovedades.FirstOrDefault(x => x.Nombre == Nombre &&
                                                                                x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                x.Id != Id);
                if (nombreExiste != null)
                {
                    errores.Add(new ValidationResult("La categoría de novedad que intentas guardar ya existe.",
                       new[] { "Nombre" }));
                }

                if (ConceptoNominaId != null)
                {
                    var conceptoExiste = dbContexto.CategoriaNovedades.FirstOrDefault(x => x.ConceptoNominaId == ConceptoNominaId &&
                                                                                        x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                        x.Id != Id);
                    if (conceptoExiste != null)
                    {
                        errores.Add(new ValidationResult("El concepto de nómina que intentas guardar ya existe. Para una categoría.",
                           new[] { "ConceptoNominaId" }));
                    }
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
