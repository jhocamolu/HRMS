using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Candidatos.Comandos.Estado
{
    public class EstadoCandidatoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public EstadoCandidato? Estado { get; set; }

        public string Justificacion { get; set; }

        public string AdjuntoPruebas { get; set; }

        public string AdjuntoExamen { get; set; }
        #endregion

        #region Validaciones Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region Id
                var existeId = contexto.Candidatos.FirstOrDefault(x => x.Id == Id);
                if (existeId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("candidato"),
                    new[] { "Id" }));
                }
                #endregion

                #region Justificacion
                if (string.IsNullOrEmpty(Justificacion) &&
                        (Estado == EstadoCandidato.Descartado ||
                        Estado == EstadoCandidato.NoApto ||
                        Estado == EstadoCandidato.Reprobado)
                   )
                {
                    errores.Add(new ValidationResult(ConstantesErrores.Requerido,
                        new[] { "Justificacion" }));
                }
                #endregion

                #region Estado
                string mesnaje = "No es posible actualizar a este estado.";

                if (Estado == EstadoCandidato.Postulado ||
                    existeId.Estado == EstadoCandidato.NoApto ||
                    existeId.Estado == EstadoCandidato.Descartado ||
                    existeId.Estado == EstadoCandidato.Reprobado)
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                    return errores;
                }

                #region Postulado
                if (existeId.Estado == EstadoCandidato.Postulado &&
                        Estado != EstadoCandidato.Competente &&
                        Estado != EstadoCandidato.Descartado
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }
                #endregion

                #region Competente
                if (existeId.Estado == EstadoCandidato.Competente &&
                        Estado != EstadoCandidato.Elegible &&
                        Estado != EstadoCandidato.Reprobado
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }
                #endregion

                #region Elegible
                if (existeId.Estado == EstadoCandidato.Elegible &&
                        Estado != EstadoCandidato.Seleccionado &&
                        Estado != EstadoCandidato.NoApto
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
                }
                else
                {
                    if (Estado == EstadoCandidato.Seleccionado)
                    {
                        int activoEnConvocatorias = (from candidato in contexto.Candidatos
                                                     join requisicion in contexto.RequisicionPersonales on
                                                     candidato.RequisicionPersonalId equals requisicion.Id
                                                     where candidato.HojaDeVidaId == existeId.HojaDeVidaId &&
                                                      candidato.Estado != EstadoCandidato.Descartado &&
                                                      candidato.Estado != EstadoCandidato.NoApto &&
                                                      candidato.Estado != EstadoCandidato.Reprobado &&
                                                      requisicion.Estado != EstadoRequisicionPersonal.Anulada &&
                                                      requisicion.Estado != EstadoRequisicionPersonal.Cancelada &&
                                                      requisicion.Estado != EstadoRequisicionPersonal.Cubierta &&
                                                      requisicion.Estado != EstadoRequisicionPersonal.Rechazada
                                                     select candidato.Id).Count()
                                    ;


                        if (activoEnConvocatorias > 1)
                        {
                            var candidato = contexto.HojaDeVidas.FirstOrDefault(x => x.Id == existeId.HojaDeVidaId);

                            errores.Add(new ValidationResult(
                                "Para poder actualizar el estado, debes descartar al candidato " +
                                $"{candidato.PrimerNombre} {candidato.SegundoNombre} {candidato.PrimerApellido} {candidato.SegundoApellido}" +
                                " en las demás convocatorias en las que se encuentra activo."
                                , new[] { "Estado" }));
                        }
                    }
                }
                #endregion

                #region Seleccionado
                if (existeId.Estado == EstadoCandidato.Seleccionado &&
                        Estado != EstadoCandidato.Elegible
                    )
                {
                    errores.Add(new ValidationResult(mesnaje, new[] { "Estado" }));
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