
using ApiV3.Dominio.TipoLiquidacionEstados.Comandos.Crear;
using ApiV3.Dominio.TipoLiquidacionEstados.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLiquidacionEstadosController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoLiquidacionEstadosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/TipoLiquidacionEstados
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTipoLiquidacionEstadoRequest comando)
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
        #endregion

        #region DELETE
        // DELETE: api/ApiWithActions/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarTipoLiquidacionEstadoRequest
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
        #endregion
    }
}
