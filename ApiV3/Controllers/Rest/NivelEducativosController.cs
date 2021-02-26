using ApiV3.Dominio.NivelEducativos.Comandos.Actualizar;
using ApiV3.Dominio.NivelEducativos.Comandos.Crear;
using ApiV3.Dominio.NivelEducativos.Comandos.Eliminar;
using ApiV3.Dominio.NivelEducativos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU030_Niveles_Educativos
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelEducativosController : ControllerBase
    {
        private readonly IMediator mediador;
        public NivelEducativosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        // PUT: api/[controller]ivelEducativos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelEducativos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarNivelEducativoRequest nivelEducativo)
        {
            if (id != nivelEducativo.Id) return BadRequest();

            var resultado = await mediador.Send(nivelEducativo);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: api/[controller]ivelEducativos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelEducativos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearNivelEducativoRequest nivelEducativo)
        {
            var resultado = await mediador.Send(nivelEducativo);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE 
        //DELETE: api/[controller]ivelEducativos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelEducativos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var nivelEducativo = new EliminarNivelEducativoRequest { Id = id };

            var resultado = await mediador.Send(nivelEducativo);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelEducativos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialNivelEducativoRequest nivelEducativo)
        {
            if (id != nivelEducativo.Id) return BadRequest();

            var resultado = await mediador.Send(nivelEducativo);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
