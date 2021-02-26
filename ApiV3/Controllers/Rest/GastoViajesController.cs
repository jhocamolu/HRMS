using ApiV3.Dominio.GastoViajes.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU0102_Administrar gastos de viaje.

namespace ApiV3.Dominio
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastoViajesController : ControllerBase
    {
        private readonly IMediator mediador;

        public GastoViajesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PATCH
        // POST: api/[controller]argos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GastoViajes_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialGastoViajeRequest comando)
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