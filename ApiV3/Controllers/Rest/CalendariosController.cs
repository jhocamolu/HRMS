using ApiV3.Dominio.Calendarios.Comandos.Actualizar;
using ApiV3.Dominio.Calendarios.Comandos.Crear;
using ApiV3.Dominio.Calendarios.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendariosController : ControllerBase
    {

        private readonly IMediator mediador;

        public CalendariosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // PUT: api/[controller]alendarios/5.
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Calendarios_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ActualizarCalendarioRequest comando)
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

        // POST: api/[controller]alendarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Calendarios_Crear })]
        [HttpPost]
        public async Task<ActionResult<Calendario>> Post([FromBody] CrearCalendarioRequest comando)
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

        // DELETE: api/[controller]alendarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Calendarios_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Calendario>> Delete(int id)
        {
            var comando = new EliminarCalendarioRequest
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
