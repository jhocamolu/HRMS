using ApiV3.Dominio.NivelCargos.Comandos.Actualizar;
using ApiV3.Dominio.NivelCargos.Comandos.Crear;
using ApiV3.Dominio.NivelCargos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU023_Administrar_Nivel_Cargos
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelCargosController : ControllerBase
    {
        private readonly IMediator mediador;

        public NivelCargosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/[controller]ivelCargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelCargos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearNivelCargoRequest comando)
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
        // PUT: api/[controller]ivelCargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelCargos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarNivelCargoRequest comando)
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
        // DELETE: api/[controller]ivelCargos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var comando = new EliminarNivelCargoRequest
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
        //PATCH: api/[controller]ivelCargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelCargos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialNivelCargoRequest comando)
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
