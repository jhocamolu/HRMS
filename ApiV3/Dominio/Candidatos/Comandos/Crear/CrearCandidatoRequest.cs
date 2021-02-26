using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Candidatos.Comandos.Crear
{
    public class CrearCandidatoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? RequisicionPersonalId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? HojaDeVidaId { get; set; }

        public string Justificacion { get; set; }

        public string AdjuntoPruebas { get; set; }

        public string AdjuntoExamen { get; set; }
        #endregion

        #region Validacion Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (NominaDbContext)validationContext.GetService(typeof(NominaDbContext));
                #region RequisicionPersonalId
                var requisicionPersonalId = contexto.RequisicionPersonales.FirstOrDefault(x => x.Id == RequisicionPersonalId);
                if (requisicionPersonalId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("requisición personal"),
                    new[] { "RequisicionPersonalId" }));
                }
                else
                {
                    if (requisicionPersonalId.Estado != EstadoRequisicionPersonal.Autorizada)
                    {
                        errores.Add(new ValidationResult("Esta convocatoria fue anulada, no es posible realizar la postulación.",
                    new[] { "RequisicionPersonalId" }));
                    }
                }
                #endregion

                #region HojaDeVidaId
                var hojaDeVidaId = contexto.HojaDeVidas.FirstOrDefault(x => x.Id == HojaDeVidaId);
                if (hojaDeVidaId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("hoja de vida"),
                    new[] { "HojaDeVidaId" }));
                }
                #endregion

                #region Existe HojaDeVidaId para la  RequisicionPersonalId
                if (requisicionPersonalId != null && hojaDeVidaId != null)
                {
                    var repetido = contexto.Candidatos
                                           .FirstOrDefault(x => x.RequisicionPersonalId == RequisicionPersonalId &&
                                                                x.HojaDeVidaId == HojaDeVidaId &&
                                                                x.EstadoRegistro == EstadoRegistro.Activo
                                                           );

                    if (repetido != null)
                    {
                        errores.Add(new ValidationResult("Esta requisición no puede tener un candidato repetido.",
                        new[] { "HojaDeVidaId" }));
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
