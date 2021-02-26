using ApiV3.Dominio.NomenclaturaDians.Comandos.Actualizar;
using ApiV3.Dominio.NomenclaturaDians.Comandos.Crear;
using ApiV3.Dominio.NomenclaturaDians.Comandos.Eliminar;
using ApiV3.Dominio.NomenclaturaDians.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclaturaDiansController : ControllerBase
    {
        private readonly IMediator mediador;

        public NomenclaturaDiansController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        // PUT: api/[controller]omenclaturaDians/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NomenclaturaDians_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarNomenclaturaDianRequest comando)
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

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NomenclaturaDians_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialNomenclaturaDianRequest comando)
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

        // POST: api/[controller]omenclaturaDians
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NomenclaturaDians_Crear })]
        [HttpPost]
        public async Task<ActionResult<NomenclaturaDian>> Crear([FromBody] CrearNomenclaturaDianRequest comando)
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

        // DELETE: api/[controller]omenclaturaDians/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NomenclaturaDians_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<NomenclaturaDian>> Eliminar(int id)
        {
            var comando = new EliminarNomenclaturaDianRequest
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
