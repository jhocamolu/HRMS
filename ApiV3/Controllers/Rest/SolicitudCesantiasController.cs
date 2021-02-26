using ApiV3.Dominio.SolicitudCesantias.Comandos.Actualizar;
using ApiV3.Dominio.SolicitudCesantias.Comandos.Crear;
using ApiV3.Dominio.SolicitudCesantias.Comandos.Estado;
using ApiV3.Dominio.SolicitudCesantias.Comandos.Parcial;
using ApiV3.Dominio.SolicitudCesantias.Consultas;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU065_SolicitudCesantias

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCesantiasController : ControllerBase
    {
        private readonly IMediator mediador;

        public SolicitudCesantiasController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region GET
        // POST: api/solicitudCesantias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudCesantias_ObtenerDatosCesantias })]
        [HttpGet("DatosCesantias/{FuncionarioId}")]
        public async Task<ActionResult<object>> Obtener(int FuncionarioId)
        {
            var otroSi = new ObtenerDatosCesantiasRequest()
            {
                FuncionarioId = FuncionarioId,
            };
            var resultado = await mediador.Send(otroSi);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion  
        #region POST
        // POST: api/solicitudCesantias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudCesantias_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearSolicitudCesantiaRequest comando)
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
        // PUT: api/solicitudCesantias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudCesantias_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarSolicitudCesantiaRequest comando)
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
        // PATCH: api/solicitudCesantias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudCesantias_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialSolicitudCesantiaRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudCesantias_CambiarEstado })]
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoSolicitudCesantiaRequest comando)
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
        #region Delete
        //DELETE: api/[controller] argos/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarSolicitudCesantiaRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }*/
        #endregion


    }
}