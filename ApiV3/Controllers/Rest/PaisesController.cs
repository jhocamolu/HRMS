using ApiV3.Dominio.Paises.Comandos.Actualizar;
using ApiV3.Dominio.Paises.Comandos.Crear;
using ApiV3.Dominio.Paises.Comandos.Eliminar;
using ApiV3.Dominio.Paises.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly IMediator mediador;

        public PaisesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // PUT: ApiV3.Paises/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Paises_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarPaisRequest comando)
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

        // POST: ApiV3.Paises
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Paises_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearPaisRequest comando)
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

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Paises_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialPaisRequest comando)
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


        // DELETE: ApiV3.Paises/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Paises_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarPaisRequest
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

    }
}
