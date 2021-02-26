using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.RepresentanteEmpresas.Comandos.Actualizar
{
    public class ActuailzarRepresentanteEmpresaRequest : IRequest<CommandResult>, IValidatableObject
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? FuncionarioId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string GrupoDocumentoSlug { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaInicio { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime? FechaFin { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id

                var existe = contexto.RepresentanteEmpresas.FirstOrDefault(x => x.Id == Id);
                if (existe == null)
                {
                    errores.Add(new ValidationResult(
                        $"No existe.",
                        new[] { "Id" }));
                }

                #endregion

                #region FuncionarioId
                if (FuncionarioId >= 0)
                {
                    var validaFuncionario = contexto.Funcionarios.FirstOrDefault(x => x.Id == FuncionarioId);
                    if (validaFuncionario == null)
                    {
                        errores.Add(new ValidationResult(
                            $"No existe.",
                            new[] { "FuncionarioId" }));
                    }
                }
                #endregion
                #region Fechas

                if (FechaInicio >= FechaFin)
                {
                    errores.Add(new ValidationResult(
                        $"La fecha fin no puede ser menor que la fecha inicio.",
                        new[] { "FechaFin" }));
                }


                var validarConRepresentante = contexto.RepresentanteEmpresas.FirstOrDefault(f =>
                                           f.Id != Id &&
                                           f.FuncionarioId == FuncionarioId &&
                                           f.GrupoDocumentoSlug == GrupoDocumentoSlug &&
                                           ((f.FechaInicio == FechaInicio && f.FechaFin == FechaFin) ||
                                           (f.FechaInicio >= FechaInicio && f.FechaFin <= FechaInicio) ||
                                           (f.FechaInicio >= FechaFin && f.FechaFin <= FechaFin) ||
                                           (FechaInicio >= FechaInicio && FechaInicio <= f.FechaFin) ||
                                           (FechaFin >= f.FechaInicio && FechaFin <= f.FechaFin))
                                          );
                if (validarConRepresentante != null)
                {
                    errores.Add(new ValidationResult(
                       $"El representante de la empresa que intentas guardar, ya existe para el grupo de documento y el rango de fechas ingresados.",
                       new[] { "SnackError" }));
                }


                var validarSinRepresentante = contexto.RepresentanteEmpresas.FirstOrDefault(f =>
                                            f.Id != Id &&
                                            f.FuncionarioId != FuncionarioId &&
                                            f.GrupoDocumentoSlug == GrupoDocumentoSlug &&
                                            ((f.FechaInicio == FechaInicio && f.FechaFin == FechaFin) ||
                                            (f.FechaInicio >= FechaInicio && f.FechaFin <= FechaInicio) ||
                                            (f.FechaInicio >= FechaFin && f.FechaFin <= FechaFin) ||
                                            (FechaInicio >= FechaInicio && FechaInicio <= f.FechaFin) ||
                                            (FechaFin >= f.FechaInicio && FechaFin <= f.FechaFin))
                                           );
                if (validarSinRepresentante != null)
                {
                    errores.Add(new ValidationResult(
                       $"El representante de la empresa que ingresaste con el grupo de documento indicado no se puede guardar, ya que la fecha inicio y/o fecha fin está entre un rango de fechas asignado.",
                       new[] { "SnackError" }));
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
