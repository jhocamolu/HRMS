using ApiV3.Dominio.Embargos.Comandos.Actualizar;
using ApiV3.Dominio.Embargos.Comandos.Crear;
using ApiV3.Dominio.Embargos.Comandos.Estado;
using ApiV3.Dominio.Embargos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU060_Administrar_Libranza
namespace ApiV3.Controllers.Rest
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmbargosController : ControllerBase
    {
        private readonly IMediator mediador;
        public EmbargosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/Embargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Embargos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearEmbargoRequest embargo)
        {
            var resultado = await mediador.Send(embargo);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PUT
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Embargos_Actualizar })]
        // PUT: api/Embargos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarEmbargosRequest embargo)
        {
            if (id != embargo.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(embargo);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region DELETE
        //DELETE: api/Embargos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var embargo = new EliminarEmbargosRequest
        //    {
        //        Id = id
        //    };
        //    var resultado = await mediador.Send(embargo);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        #endregion

        #region PATCH
        // PATCH: api/Embargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Embargos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialEmbargosRequest embargos)
        {
            if (id != embargos.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(embargos);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Embargos_CambiarEstado })]
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoEmbargoRequest comando)
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
