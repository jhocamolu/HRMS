using ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Actualizar;
using ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Crear;
using ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Eliminar;
using ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Parcial;
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
    public class DivisionPoliticaNiveles1Controller : ControllerBase
    {
        private readonly IMediator mediador;

        public DivisionPoliticaNiveles1Controller(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // PUT: ApiV3.DivisionPoliticaNiveles1/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles1_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarDivisionPoliticaNivel1Request comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles1_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialDivisionPoliticaNivel1Request comando)
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
        // POST: ApiV3.DivisionPoliticaNiveles1
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles1_Crear })]
        [HttpPost]
        public async Task<ActionResult<DivisionPoliticaNivel1>> Crear(CrearDivisionPoliticaNivel1Request comando)
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

        // DELETE: ApiV3.DivisionPoliticaNiveles1/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles1_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DivisionPoliticaNivel1>> Eliminar(int id)
        {
            var comando = new EliminarDivisionPoliticaNivel1Request
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
