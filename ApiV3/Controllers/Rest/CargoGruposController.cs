using ApiV3.Dominio.CargoGrupos.Comandos.Crear;
using ApiV3.Dominio.CargoGrupos.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU025_CargoReporta
/// 
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoGruposController : ControllerBase
    {

        private readonly IMediator mediador;

        public CargoGruposController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/CargoGrupo
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoGrupos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearCargoGrupoRequest comando)
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

        // DELETE: api/CargoGrupo/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoGrupos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id, [FromBody] EliminarCargoGrupoRequest comando)
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
    }
}
