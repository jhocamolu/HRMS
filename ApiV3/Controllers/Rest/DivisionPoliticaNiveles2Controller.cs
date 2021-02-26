using ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Actualizar;
using ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Crear;
using ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Eliminar;
using ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Parcial;
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
    public class DivisionPoliticaNiveles2Controller : ControllerBase
    {
        private readonly IMediator mediador;

        public DivisionPoliticaNiveles2Controller(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // PUT: ApiV3.DivisionPoliticaNiveles2/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles2_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarDivisionPoliticaNivel2Request comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles2_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialDivisionPoliticaNivel2Request comando)
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
        // POST: ApiV3.DivisionPoliticaNiveles2
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles2_Crear })]
        [HttpPost]
        public async Task<ActionResult<DivisionPoliticaNivel2>> Crear(CrearDivisionPoliticaNivel2Request comando)
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

        // DELETE: ApiV3.DivisionPoliticaNiveles2/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles2_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DivisionPoliticaNivel2>> Eliminar(int id)
        {
            var comando = new EliminarDivisionPoliticaNivel2Request
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
