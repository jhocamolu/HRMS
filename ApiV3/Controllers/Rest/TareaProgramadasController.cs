using ApiV3.Dominio.TareaProgramadas.Comandos.Actualizar;
using ApiV3.Dominio.TareaProgramadas.Comandos.Crea;
using ApiV3.Dominio.TareaProgramadas.Comandos.Ejecutar;
using ApiV3.Dominio.TareaProgramadas.Comandos.Eliminar;
using ApiV3.Dominio.TareaProgramadas.Comandos.Parcial;
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
    public class TareaProgramadasController : ControllerBase
    {
        private readonly IMediator mediador;
        public TareaProgramadasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/TareaProgramadas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarTareaProgramadaRequest actualizarTarea)
        {
            if (id != actualizarTarea.Id) return BadRequest();
            var resultado = await mediador.Send(actualizarTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.TareaProgramadas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadas_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTareaProgramadaRequest crearTarea)
        {
            var resultado = await mediador.Send(crearTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.TareaProgramadas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadas_CambiarEstadoRegistro })]
        [HttpPatch("{alias}")]
        public async Task<ActionResult> Parcial(string alias, [FromBody] ParcialTareaProgramadaRequest parcialTarea)
        {
            if (alias != parcialTarea.Alias) return BadRequest();

            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        // DELETE: api/TareaProgramadas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadaLogs_Crear })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var beneficio = new EliminarTareaProgramadaRequest
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

        #region POST EJECUTAR
        // POST: ApiV3.TareaProgramadas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadaLogs_Crear })]
        [HttpPost("{alias}/Ejecutar")]
        public async Task<ActionResult> Ejecutar(string alias)
        {
            var crearTarea = new EjecutarTareaProgramadaRequest() { Alias = alias };
            var resultado = await mediador.Send(crearTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
