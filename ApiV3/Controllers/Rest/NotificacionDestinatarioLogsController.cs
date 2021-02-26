using ApiV3.Dominio.NotificacionDestinatarioLogs.Comandos.Crear;
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
    public class NotificacionDestinatarioLogsController : ControllerBase
    {
        private readonly IMediator mediador;

        public NotificacionDestinatarioLogsController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/NotificacionDestinatarioLogs
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarioLogs_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearNotificacionDestinatarioLogRequest crearNotificacionDestinatarioLog)
        {
            var resultado = await mediador.Send(crearNotificacionDestinatarioLog);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        //#region DELETE
        //// DELETE: api/NotificacionDestinatarioLogs/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var eliminarNotificacionDestinatarioLog = new EliminarNotificacionDestinatarioLogRequest
        //    {
        //        Id = id
        //    };
        //    var resultado = await mediador.Send(eliminarNotificacionDestinatarioLog);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        //#endregion
    }
}
