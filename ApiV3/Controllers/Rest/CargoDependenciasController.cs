using ApiV3.Dominio.CargoDependencias.Comandos.Crear;
using ApiV3.Dominio.CargoDependencias.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU013_Actividades_Economicas


namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDependenciasController : ControllerBase
    {
        private readonly IMediator mediador;

        public CargoDependenciasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/[controller]argoDependencias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoDependencias_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearCargoDependenciaRequest comando)
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

        //DELETE: api/[controller]argoDependencias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoDependencias_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarCargoDependenciaRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
    }
}
