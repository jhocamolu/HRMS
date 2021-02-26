using ApiV3.Dominio.Notificaciones.Comandos.Actualizar;
using ApiV3.Dominio.Notificaciones.Comandos.Crear;
using ApiV3.Dominio.Notificaciones.Comandos.Ejecutar;
using ApiV3.Dominio.Notificaciones.Comandos.Parcial;
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
    public class NotificacionesController : ControllerBase
    {
        private readonly IMediator mediador;

        public NotificacionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Notificaciones_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarNotificacionRequest actualizarNotificacion)
        {
            if (id != actualizarNotificacion.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(actualizarNotificacion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: api/Beneficios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Notificaciones_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearNotificacionRequest crearNotificacion)
        {
            var resultado = await mediador.Send(crearNotificacion);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        // PATCH: api/[Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Notificaciones_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialNotificacionRequest parcialNotificacion)
        {
            if (id != parcialNotificacion.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(parcialNotificacion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        //#region DELETE
        //// DELETE: api/Beneficios/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var beneficio = new EliminarNotificacionRequest
        //    {
        //        Id = id
        //    };
        //    var resultado = await mediador.Send(beneficio);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        //#endregion

        // POST: api/Nominas

        #region POST EJECUTAR
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Notificaciones_Ejecutar })]
        [HttpPost("{id}/Ejecutar")]
        public async Task<ActionResult> Ejecutar(int id)
        {

            var notificacion = new EjecutarNotificacionRequest() { Id = id };
            var resultado = await mediador.Send(notificacion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
