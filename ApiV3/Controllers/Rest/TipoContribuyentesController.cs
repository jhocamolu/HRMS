using ApiV3.Dominio.TipoContribuyentes.Comandos.Actualizar;
using ApiV3.Dominio.TipoContribuyentes.Comandos.Crear;
using ApiV3.Dominio.TipoContribuyentes.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU021_Administrar_Tipo_Contribuyente

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoContribuyentesController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoContribuyentesController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region POST
        // POST: api/[controller]ipoContribuyentes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoContribuyentes_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTipoContribuyenteRequest comando)
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
        // PUT: api/[controller]ipoContribuyentes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoContribuyentes_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarTipoContribuyenteRequest comando)
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
        // DELETE: api/[controller]ipoContribuyentes/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var comando = new EliminarTipoContribuyenteRequest
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

        #region PATCH
        // PATCH: api/[controller]ipoContribuyentes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoContribuyentes_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialTipoContribuyenteRequest comando)
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
