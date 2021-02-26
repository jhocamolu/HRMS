using ApiV3.Dominio.TipoEmbargos.Comandos.Actualizar;
using ApiV3.Dominio.TipoEmbargos.Comandos.Crear;
using ApiV3.Dominio.TipoEmbargos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU051_Tipo_Embargos

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEmbargosController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoEmbargosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/[controller]ipoEmbargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTipoEmbargoRequest comando)
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
        // PUT: api/[controller]ipoEmbargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarTipoEmbargoRequest comando)
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
        //PATCH: api/[controller]ipoEmbargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialTipoEmbargoRequest comando)
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
        // DELETE: api/[controller]ipoEmbargos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var comando = new EliminarTipoEmbargoRequest
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