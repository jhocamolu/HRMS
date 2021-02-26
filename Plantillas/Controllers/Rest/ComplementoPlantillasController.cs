using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Dominio.ComplementoPlantillas.Comandos.Actualizar;
using Plantillas.Dominio.ComplementoPlantillas.Comandos.Crear;
using Plantillas.Dominio.ComplementoPlantillas.Comandos.Eliminar;
using Plantillas.Dominio.ComplementoPlantillas.Comandos.Parcial;
using Plantillas.Models;
using System.Threading.Tasks;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplementoPlantillasController : ControllerBase
    {
        private readonly IMediator mediador;

        public ComplementoPlantillasController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region PUT
        // PUT: api/ComplementoPlantillas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarComplementoPlantillaRequest comando)
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
        #region PATCH
        // PATCH: api/Cargos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialComplementoPlantillaRequest comando)
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
        #region POST
        // POST: api/ComplementoPlantillas
        [HttpPost]
        public async Task<ActionResult<ComplementoPlantilla>> Crear([FromBody] CrearComplementoPlantillaRequest comando)
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
        // DELETE: api/ComplementoPlantillas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComplementoPlantilla>> Eliminar(int id)
        {
            var comando = new EliminarComplementoPlantillaRequest
            {
                Id = id
            };
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
