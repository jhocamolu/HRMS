using ApiV3.Dominio.SolicitudPermisos.Comandos.Actualizar;
using ApiV3.Dominio.SolicitudPermisos.Comandos.Crear;
using ApiV3.Dominio.SolicitudPermisos.Comandos.Estado;
using ApiV3.Dominio.SolicitudPermisos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU066_SolicitudPermisos
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudPermisosController : ControllerBase
    {
        private readonly IMediator mediador;

        public SolicitudPermisosController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region POST
        // POST: api/SolicitudPermisos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudPermisos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearSolicitudPermisoRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion  
        #region PUT
        // PUT: api/SolicitudPermisos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudPermisos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarSolicitudPermisoRequest comando)
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

        #region PATCH
        // PATCH: api/solicitudPermisos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudPermisos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialSolicitudPermisoRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudPermisos_CambiarEstado })]
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoSolicitudPermisoRequest comando)
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
    }
}
