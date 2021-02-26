using ApiV3.Dominio.LiquidacionConceptos.Comandos.Crear;
using ApiV3.Dominio.LiquidacionConceptos.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLiquidacionConceptosController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoLiquidacionConceptosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: api/[controller]ipoLiquidacions
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionConceptos_Crear })]
        [HttpPost]
        public async Task<ActionResult<TipoLiquidacion>> Crear([FromBody] CrearTipoLiquidacionConceptoRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        // DELETE: api/[controller]ipoLiquidacions/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionConceptos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoLiquidacion>> Eliminar(int id)
        {
            var comando = new EliminarTipoLiquidacionConceptoRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
    }
}
