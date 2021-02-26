using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Cargos.Crear
{
    public class CrearCargoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + " 40.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico+ ConstantesExpresionesRegulares.Alfabetico +  " ]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string ObjetivoCargo { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int NivelCargoId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? CostoSicom { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public ClaseCargo? Clase { get; set; }

        public int? GrupoId { get; set; }

        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Código
                //Valida que código sea único

                var validaCodigo = dbContexto.Cargos.FirstOrDefault(x => x.Codigo == Codigo);
                if (validaCodigo != null)
                {
                    errores.Add(new ValidationResult(
                        $"El código que intentas guardar ya existe.",
                        new[] { "Codigo" }));
                }

                #endregion
                #region Nombre
                //Valida que nombre sea único
                var validaNombre = dbContexto.Cargos.FirstOrDefault(x => x.Nombre == Nombre);
                if (validaNombre != null)
                {
                    errores.Add(new ValidationResult(
                        $"El nombre que intentas guardar ya existe.",
                        new[] { "Nombre" }));
                }
                #endregion
                #region Nivel Cargo
                //Valida que nivel Cargo existte
                var existeNivelCargo = dbContexto.NivelCargos.FirstOrDefault(x => x.Id == NivelCargoId);
                if (existeNivelCargo == null)
                {
                    errores.Add(new ValidationResult(
                        $"El nivel del cargo no existe.",
                        new[] { "NivelCargoId" }));
                }
                #endregion
                #region 
                // Consulta el grupo con menor orden

                var consultaGrupo = dbContexto.Grupos.Where(x => x.EstadoRegistro == EstadoRegistro.Activo)
                                                        .OrderBy(x => x.Orden)
                                                        .FirstOrDefault();
                GrupoId = consultaGrupo.Id;

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
