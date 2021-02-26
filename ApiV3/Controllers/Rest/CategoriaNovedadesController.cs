using ApiV3.Dominio.CategoriaNovedades.Comandos.Actualizar;
using ApiV3.Dominio.CategoriaNovedades.Comandos.Crear;
using ApiV3.Dominio.CategoriaNovedades.Comandos.Eliminar;
using ApiV3.Dominio.CategoriaNovedades.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU090_AdmininstrarCategoriaNovedad
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaNovedadesController : ControllerBase
    {
        private readonly IMediator mediador;

        public CategoriaNovedadesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/CategoriaNovedades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaNovedades_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearCategoriaNovedadRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaNovedades_Actualizar })]
        // PUT: api/CategoriaNovedades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarCategoriaNovedadRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaNovedades_CambiarEstadoRegistro })]
        // PATCH: api/CategoriaNovedades/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialCategoriaNovedadRequest comando)
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

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaNovedades_Eliminar })]
        // DELETE: api/CategoriaNovedades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarCategoriaNovedadRequest
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