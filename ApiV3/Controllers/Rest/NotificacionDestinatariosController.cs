using ApiV3.Dominio.NotificacionDestinatarios.Comandos.Crear;
using ApiV3.Dominio.NotificacionDestinatarios.Comandos.Eliminar;
using ApiV3.Dominio.NotificacionDestinatarios.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// Controlador
/// Sprint8

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionDestinatariosController : ControllerBase
    {
        private readonly IMediator mediador;

        public NotificacionDestinatariosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/NotificacionDestinatario
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarios_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearNotificacionDestinatarioRequest crearNotificacionDestinatario)
        {
            var resultado = await mediador.Send(crearNotificacionDestinatario);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        // DELETE: api/NotificacionDestinatario/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarios_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var beneficio = new EliminarNotificacionDestinatarioRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.funcionario/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarios_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialNotificacionDestinatarioRequest notificacionDestinatario)
        {
            if (id != notificacionDestinatario.Id) return BadRequest();

            var resultado = await mediador.Send(notificacionDestinatario);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
