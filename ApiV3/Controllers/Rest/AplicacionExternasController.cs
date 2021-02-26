using ApiV3.Dominio.AplicacionExternas.Comandos.Actualizar;
using ApiV3.Dominio.AplicacionExternas.Comandos.Crear;
using ApiV3.Dominio.AplicacionExternas.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU069_AplicacionExterna

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionExternasController : ControllerBase
    {
        private readonly IMediator mediador;

        public AplicacionExternasController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region POST
        // POST: api/[controller]argos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternas_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearAplicacionExternaRequest comando)
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
        // PUT: api/[controller]argos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarAplicacionExternaRequest comando)
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
        // PATCH: api/[controller]argos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialAplicacionExternaRequest comando)
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
        // DELETE: api/[controller]argos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var comando = new EliminarAplicacionExternaRequest
        //    {
        //        Id = id
        //    };
        //    var resultado = await mediador.Send(comando);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        #endregion

    }
}
