using ApiV3.Dominio.Archivos.Consultas;
using ApiV3.Dominio.Archivos.Crear;
using ApiV3.Dominio.Archivos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description Controlador administra los recursos hacia la ApiV3.de Archivos.

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly IMediator mediador;

        public ArchivosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region GET
        // GET: ApiV3.Archivos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Archivos_Obtener })]
        [HttpGet("{Id}/Archivo")]
        public async Task<ActionResult<object>> Obtener(string Id)
        {
            var comando = new ObtenerArchivoRequest()
            {
                Id = Id,
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion
        #region POST
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Archivos_Crear })]
        // POST: ApiV3.Archivos
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearArchivoRequest comando)
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

        #region DELETE
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Archivos_Eliminar })]
        // DELETE: api/[controller]argos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(string id)
        {
            var comando = new EliminarArchivoRequest
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
        #endregion
    }
}
