using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Cargos.Parcial
{
    public class ParcialCargoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        public bool? Activo { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(10, ErrorMessage = ConstantesErrores.Maximo + " 10.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + "]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Codigo { get; set; }

        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + " 1.")]
        [MaxLength(40, ErrorMessage = ConstantesErrores.Maximo + " 40.")]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfanumerico + ConstantesExpresionesRegulares.Alfabetico + " ]*$", ErrorMessage = ConstantesErrores.Alfanumerico)]
        public string Nombre { get; set; }

        public string ObjetivoCargo { get; set; }

        public int? NivelCargoId { get; set; }


        public bool? CostoSicom { get; set; }


        [EnumDataType(typeof(ClaseCargo), ErrorMessage = "No es una clase de cargo válido.")]
        public ClaseCargo? Clase { get; set; }
        #endregion

        #region Validaciones Manuales

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var dbContexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                var elemento = dbContexto.Cargos.FirstOrDefault(x => x.Id == Id);
                if (elemento == null)
                {
                    errores.Add(new ValidationResult(
                       $"No Existe.",
                       new[] { "Id" }));

                    return errores;
                }
                #endregion
                #region Código
                //Valida que código sea único
                if (Codigo != null)
                {
                    var validaCodigo = dbContexto.Cargos.FirstOrDefault(x => x.Codigo == Codigo && x.Id != Id);
                    if (validaCodigo != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El código que intentas guardar ya existe.",
                            new[] { "Codigo" }));
                    }
                }
                #endregion
                #region Nombre
                //Valida que nombre sea único
                if (Nombre != null)
                {
                    var validaNombre = dbContexto.Cargos.FirstOrDefault(x => x.Nombre == Nombre && x.Id != Id);
                    if (validaNombre != null)
                    {
                        errores.Add(new ValidationResult(
                            $"El nombre que intentas guardar ya existe.",
                            new[] { "Nombre" }));
                    }
                }
                #endregion

                #region Nivel Cargo
                //Valida que nivel Cargo existte
                if (NivelCargoId != null)
                {
                    var existeNivelCargo = dbContexto.NivelCargos.FirstOrDefault(x => x.Id == NivelCargoId);
                    if (existeNivelCargo == null)
                    {
                        errores.Add(new ValidationResult(
                            $"El nivel del cargo no existe.",
                            new[] { "NivelCargoId" }));
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
