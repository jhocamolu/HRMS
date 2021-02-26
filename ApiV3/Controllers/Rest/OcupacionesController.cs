using ApiV3.Dominio.Ocupaciones.Comandos.Actualizar;
using ApiV3.Dominio.Ocupaciones.Comandos.Crear;
using ApiV3.Dominio.Ocupaciones.Comandos.Eliminar;
using ApiV3.Dominio.Ocupaciones.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU006_Administrar_Ocupaciones
/// 
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcupacionesController : ControllerBase
    {
        private readonly IMediator mediador;

        public OcupacionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        // PUT: api/[controller]cupaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Ocupaciones_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarOcupacionRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region POST
        // POST: api/[controller]cupaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Ocupaciones_Crear })]
        [HttpPost]
        public async Task<ActionResult<Ocupacion>> Crear([FromBody] CrearOcupacionRequest command)
        {
            var resultado = await mediador.Send(command);
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

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Ocupaciones_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialOcupacionRequest comando)
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
        // DELETE: api/[controller]cupaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Ocupaciones_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ocupacion>> Eliminar(int id)
        {
            var command = new EliminarOcupacionRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
    }
}
