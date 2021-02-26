using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Dominio.Plantillas.Comandos.Actualizar;
using Plantillas.Dominio.Plantillas.Comandos.Crear;
using Plantillas.Dominio.Plantillas.Comandos.Eliminar;
using Plantillas.Dominio.Plantillas.Comandos.Parcial;
using Plantillas.Models;
using System.Threading.Tasks;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantillasController : ControllerBase
    {
        private readonly IMediator mediador;

        public PlantillasController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region PUT
        // PUT: api/Plantillas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarPlantillaRequest comando)
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
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialPlantillaRequest comando)
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
        // POST: api/Plantillas
        [HttpPost]
        public async Task<ActionResult<Plantilla>> Crear([FromBody] CrearPlantillaRequest comando)
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
        // DELETE: api/Plantillas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Plantilla>> Eliminar(int id)
        {
            var comando = new EliminarPlantillaRequest
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
