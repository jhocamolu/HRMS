using ApiV3.Dominio.BeneficioAdjuntos.Comandos.Eliminar;
using ApiV3.Dominio.BeneficioAdjuntos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficioAdjuntosController : ControllerBase
    {
        private readonly IMediator mediador;

        public BeneficioAdjuntosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PATCH
        // PATCH: api/[Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.BeneficioAdjuntos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialBeneficioAdjuntosRequest beneficio)
        {
            if (id != beneficio.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region DELETE
        // DELETE: api/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.BeneficioAdjuntos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var beneficio = new EliminarBeneficioAdjuntoRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
    }
}
