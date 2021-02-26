using ApiV3.Dominio.TipoLiquidaciones.Comandos.Actualizar;
using ApiV3.Dominio.TipoLiquidaciones.Comandos.Crear;
using ApiV3.Dominio.TipoLiquidaciones.Comandos.Eliminar;
using ApiV3.Dominio.TipoLiquidaciones.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLiquidacionesController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoLiquidacionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // PUT: api/[controller]ipoLiquidacions/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarTipoLiquidacionRequest comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        // POST: api/[controller]ipoLiquidacions
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_Crear })]
        [HttpPost]
        public async Task<ActionResult<TipoLiquidacion>> Crear([FromBody] CrearTipoLiquidacionRequest comando)
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

        #region PATCH
        // PATCH: api/[controller]ipomonedas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialTipoLiquidacionRequest comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        // DELETE: api/[controller]ipoLiquidacions/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoLiquidacion>> Eliminar(int id)
        {
            var comando = new EliminarTipoLiquidacionRequest
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
