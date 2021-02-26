using ApiV3.Dominio.ActividadCentroCostos.comandos.Actualizar;
using ApiV3.Dominio.ActividadCentroCostos.comandos.Crear;
using ApiV3.Dominio.ActividadCentroCostos.comandos.Eliminar;
using ApiV3.Dominio.ActividadCentroCostos.comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU070_AdmininstrarDistribucionCosto

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadCentroCostosController : ControllerBase
    {
        private readonly IMediator mediador;

        public ActividadCentroCostosController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region POST
        // POST: api/Actividades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadCentroCostos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearActividadCentroCostoRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadCentroCostos_Crear })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarActividadCentroCostoRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadCentroCostos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialActividadCentroCostoRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadCentroCostos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarActividadCentroCostoRequest
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
