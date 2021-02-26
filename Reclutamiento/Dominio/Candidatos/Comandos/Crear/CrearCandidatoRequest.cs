
using MediatR;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Infraestructura.Enumerador;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Reclutamiento.Dominio.Candidatos.Comandos.Crear
{
    public class CrearCandidatoRequest : IRequest<CommandResult>, IValidatableObject
    {
        #region validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? RequisicionPersonalId { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? HojaDeVidaId { get; set; }

        #endregion

        #region Validacion Manuales
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            try
            {
                var contexto = (ReclutamientoDbContext)validationContext.GetService(typeof(ReclutamientoDbContext));
                

                #region RequisicionPersonalId
                
                var requisicionPersonalId = contexto.RequisicionPersonales.FirstOrDefault(x => x.Id == RequisicionPersonalId);
                if (requisicionPersonalId == null)
                {
                    errores.Add(new ValidationResult(ConstantesErrores.NoExiste("requisición personal"),
                    new[] { "RequisicionPersonalId" }));
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
                                                                x.EstadoRegistro == ApiV3.Infraestructura.Enumerador.EstadoRegistro.Activo
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
