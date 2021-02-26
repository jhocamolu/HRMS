using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.DeduccionRetefuentes.Comandos.Crear
{
    public class CrearDeduccionRetefuenteRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? AnnoVigenciaId { get; set; }

        [Range(0, 100000000, ErrorMessage = ConstantesErrores.Rango + "0 - 100000000.")]
        public double? InteresVivienda { get; set; }

        [Range(0, 100000000, ErrorMessage = ConstantesErrores.Rango + "0 - 100000000.")]
        public double? MedicinaPrepagada { get; set; }

        #endregion

        #region Validaciones Manueales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region FuncionarioId
                var funcionarioId = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                if (funcionarioId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("funcionario"),
                        new[] { "FuncionarioId" }));
                }
                else
                {
                    var deduccion = contexto.DeduccionRetefuentes.FirstOrDefault(x =>
                                                                    x.FuncionarioId == FuncionarioId &&
                                                                    x.AnnoVigenciaId == AnnoVigenciaId);
                    if (deduccion != null)
                    {
                        errores.Add(new ValidationResult("Las deducciones que intentas ingresar ya existen.",
                        new[] { "AnnoVigenciaId" }));
                        return errores;
                    }
                }
                #endregion

                #region AnnoVigencias
                var annoVigenciaId = contexto.AnnoVigencias.FirstOrDefault(x => x.Id == AnnoVigenciaId);
                if (annoVigenciaId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("vigencia para este año"),
                        new[] { "annoVigenciaId" }));
                }
                else if (annoVigenciaId.Estado != EstadoAnnoVigencia.Vigente)
                {
                    errores.Add(new ValidationResult("Vigencia para este año cerrada.",
                    new[] { "annoVigenciaId" }));

                }
                #endregion

                if (InteresVivienda == null)
                {
                    InteresVivienda = 0;
                }
                if (MedicinaPrepagada == null)
                {
                    MedicinaPrepagada = 0;
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
