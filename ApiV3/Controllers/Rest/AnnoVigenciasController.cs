using ApiV3.Dominio.AnnoTrabajos.Comandos.Crear;
using ApiV3.Dominio.AnnoTrabajos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU098_Administrar_Años_Trabajo

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnoVigenciasController : ControllerBase
    {
        private readonly IMediator mediador;

        public AnnoVigenciasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/AnnoVigencias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AnnoVigencias_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearAnnoVigenciaRequest comando)
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
        #endregion

        #region PATCH
        // POST: api/[controller]argos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AnnoVigencias_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialAnnoVigenciaRequest comando)
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