using ApiV3.Dominio.FuncionarioEstudios.Comandos.Actualizar;
using ApiV3.Dominio.FuncionarioEstudios.Comandos.Crear;
using ApiV3.Dominio.FuncionarioEstudios.Comandos.Eliminar;
using ApiV3.Dominio.FuncionarioEstudios.Comandos.Parcial;
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
    public class FuncionarioEstudiosController : ControllerBase
    {
        private readonly IMediator mediador;

        public FuncionarioEstudiosController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        // PUT: ApiV3.FuncionarioEstudios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioEstudios_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarFuncionarioEstudioRequest comando)
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

        // POST: ApiV3.FuncionarioEstudios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioEstudios_Crear })]
        [HttpPost]
        public async Task<ActionResult<FuncionarioEstudio>> Crear([FromBody] CrearFuncionarioEstudioRequest comando)
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

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioEstudios_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialFuncionarioEstudioRequest comando)
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

        // DELETE: ApiV3.FuncionarioEstudios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioEstudios_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<FuncionarioEstudio>> Eliminar(int id)
        {
            var comando = new EliminarFuncionarioEstudioRequest
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
