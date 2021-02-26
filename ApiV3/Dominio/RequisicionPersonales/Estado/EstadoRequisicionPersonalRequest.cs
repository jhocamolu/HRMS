using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.RequisicionPersonales.Estado
{
    public class EstadoRequisicionPersonalRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region Validacion
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public EstadoRequisicionPersonal? Estado { get; set; }

        [MaxLength(500, ErrorMessage = ConstantesErrores.Maximo + "500.")]
        public string Justificacion { get; set; }
        #endregion

        #region Validacion Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                RequisicionPersonal existeId = contexto.RequisicionPersonales.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult("No existe.", new[] { "Id" }));
                    return errores;
                }
                #endregion

                #region Justificacion
                if (string.IsNullOrEmpty(Justificacion) &&
                        (Estado == EstadoRequisicionPersonal.Anulada ||
                        Estado == EstadoRequisicionPersonal.Cancelada)
                   )
                {
                    errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                        new[] { "Justificacion" }));
                }
                #endregion

                #region Estado
                string mesnaje = "No es posible actualizar a este estado.";

                if (Estado == EstadoRequisicionPersonal.Solicitada)
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }

                #region Estado Actual Solicitada
                if (existeId.Estado == EstadoRequisicionPersonal.Solicitada &&
                        Estado != EstadoRequisicionPersonal.Rechazada &&
                        Estado != EstadoRequisicionPersonal.Cancelada &&
                        Estado != EstadoRequisicionPersonal.Revisada
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }
                #endregion

                #region Estado Actual Revisada
                if (existeId.Estado == EstadoRequisicionPersonal.Revisada &&
                        Estado != EstadoRequisicionPersonal.Rechazada &&
                        Estado != EstadoRequisicionPersonal.Aprobada &&
                        Estado != EstadoRequisicionPersonal.Anulada
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }
                #endregion

                #region Estado Actual Aprobada
                if (existeId.Estado == EstadoRequisicionPersonal.Aprobada &&
                        Estado != EstadoRequisicionPersonal.Autorizada &&
                        Estado != EstadoRequisicionPersonal.Anulada &&
                        Estado != EstadoRequisicionPersonal.Rechazada
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }
                #endregion

                #region Estado Actual Autorizada
                if (existeId.Estado == EstadoRequisicionPersonal.Autorizada &&
                        Estado != EstadoRequisicionPersonal.Rechazada &&
                        Estado != EstadoRequisicionPersonal.Cubierta &&
                        Estado != EstadoRequisicionPersonal.Anulada
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }
                else
                {
                    if (Estado == EstadoRequisicionPersonal.Cubierta)
                    {
                        int vacantesRequisicion = existeId.Cantidad;
                        int candidatosSelecionados = contexto
                                                .Candidatos
                                                .Where(x => x.RequisicionPersonalId == Id &&
                                                            x.Estado == EstadoCandidato.Seleccionado &&
                                                            x.EstadoRegistro == EstadoRegistro.Activo)
                                                .Count();

                        if (candidatosSelecionados > vacantesRequisicion)
                        {
                            errores.Add(new ValidationResult(
                                "No puedes cerrar la requisición, el número de candidatos seleccionados " +
                                "sobrepasan el número de vacantes.",
                                new[] { "snackError" }));
                        }
                        else if (candidatosSelecionados == 0)
                        {
                            errores.Add(new ValidationResult(
                                "No puedes cerrar la requisición, porque no existen candidatos seleccionados.",
                                new[] { "snackError" }));
                        }
                    }
                }
                #endregion

                #region Estado Actual Rechazada Cancelada Anulada Cubierta
                if (existeId.Estado == EstadoRequisicionPersonal.Rechazada ||
                    existeId.Estado == EstadoRequisicionPersonal.Cancelada ||
                    existeId.Estado == EstadoRequisicionPersonal.Anulada ||
                    existeId.Estado == EstadoRequisicionPersonal.Cubierta
                    )
                {
                    errores.Add(new ValidationResult("No es posible cambiar el estado.", new[] { "Estado" }));
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
