using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.Candidatos.Comandos.Actualizar
{
    public class ActualizarCandidatoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? RequisicionPersonalId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? HojaDeVidaId { get; set; }

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

                #region RequisicionPersonalId
                if (RequisicionPersonalId != null)
                {
                    var requisicionPersonalId = contexto.RequisicionPersonales.FirstOrDefault(x => x.Id == RequisicionPersonalId);
                    if (requisicionPersonalId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("requisicion personal"),
                        new[] { "RequisicionPersonalId" }));
                    }
                }
                #endregion

                #region HojaDeVidaId
                if (HojaDeVidaId != null)
                {
                    var hojaDeVidaId = contexto.HojaDeVidas.FirstOrDefault(x => x.Id == HojaDeVidaId);
                    if (hojaDeVidaId == null)
                    {
                        errores.Add(new ValidationResult(ConstantesErrores.NoExiste("hoja de vida"),
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
