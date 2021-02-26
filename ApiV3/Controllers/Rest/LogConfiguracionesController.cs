using ApiV3.Dominio.LogConfiguraciones.Comandos;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description Controlador administra la configuracion del log

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogConfiguracionesController : ControllerBase
    {
        private readonly IMediator mediador;
        public LogConfiguracionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PATCH
        //PATCH: api/[controller]ogConfiguraciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LogConfiguraciones_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialLogConfiguracionRequest comando)
        {
            if (id != comando.Id) return BadRequest();

            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
