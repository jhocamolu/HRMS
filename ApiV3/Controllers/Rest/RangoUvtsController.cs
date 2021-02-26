using ApiV3.Dominio.RangoUvts.Comandos.Actualizar;
using ApiV3.Dominio.RangoUvts.Comandos.Crear;
using ApiV3.Dominio.RangoUvts.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU079_AdmininstrarRangoUvt

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class RangoUvtsController : ControllerBase
    {
        private readonly IMediator mediador;

        public RangoUvtsController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/Actividades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RangoUvts_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearRangoUvtRequest comando)
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

        #region PUT
        // PUT: api/Actividades/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RangoUvts_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarRangoUvtRequest comando)
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
        // PATCH: api/Actividades/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RangoUvts_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialRangoUvtRequest comando)
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

        #region DELETE
        //DELETE: api/[controller] argos/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarRangoUvtRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }*/
        #endregion
    }
}
