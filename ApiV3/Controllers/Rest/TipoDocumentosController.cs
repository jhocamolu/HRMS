using ApiV3.Dominio.TipoDocumentos.Comandos.Actualizar;
using ApiV3.Dominio.TipoDocumentos.Comandos.Crear;
using ApiV3.Dominio.TipoDocumentos.Comandos.Estado;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU011_Administrar_Tipos_Documento
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentosController : ControllerBase
    {

        private readonly IMediator mediador;
        public TipoDocumentosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        // put: api/[controller]ipodocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoDocumentos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarTipoDocumentoRequest tipoDocumento)
        {
            if (id != tipoDocumento.Id)
            {
                return BadRequest();
            }

            var actualizar = await mediador.Send(tipoDocumento);
            if (actualizar.IsSuccess) return Ok(actualizar.Data);
            return StatusCode(500, new { Message = actualizar.FailureReason });
        }
        #endregion

        #region POST
        // POST: api/[controller]ipoDocumentos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoDocumentos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTipoDocumentoRequest tipoDocumento)
        {
            var resultado = await mediador.Send(tipoDocumento);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        // DELETE: api/[controller]ipoDocumentos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var tipoDocumento = new EliminarTipoDocumentoRequest { Id = id };

        //    var resultado = await mediador.Send(tipoDocumento);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        //}
        #endregion

        #region PATCH
        //PATCH api/[controller]ipoDocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoDocumentos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialTipoDocumentoRequest tipodocumento)
        {
            if (id != tipodocumento.Id)
            {
                return BadRequest();
            }

            var resultado = await mediador.Send(tipodocumento);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion
    }
}
