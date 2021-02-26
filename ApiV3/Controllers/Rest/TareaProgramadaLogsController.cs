using ApiV3.Dominio.TareaProgramadaLogs.Commandos.Crear;
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
    public class TareaProgramadaLogsController : ControllerBase
    {
        private readonly IMediator mediador;
        public TareaProgramadaLogsController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: ApiV3.TareaProgramadasLogs
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadaLogs_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTareaProgramadaLogRequest crearTareaLog)
        {
            var resultado = await mediador.Send(crearTareaLog);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        //#region DELETE
        //// DELETE: api/TareaProgramadasLogs/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var beneficio = new EliminarTareaProgramadaLogRequest
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
    }
}