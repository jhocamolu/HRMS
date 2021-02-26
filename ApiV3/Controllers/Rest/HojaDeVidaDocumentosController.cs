using ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Actualizar;
using ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Crear;
using ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Eliminar;
using ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU099 controlador  HojaDeVidas

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class HojaDeVidaDocumentosController : ControllerBase
    {
        private readonly IMediator mediador;
        public HojaDeVidaDocumentosController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region PUT
        //PUT: ApiV3.HojaDeVidaDocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaDocumentos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaDocumentoRequest documento)
        {
            if (id != documento.Id) return BadRequest();
            var resultado = await mediador.Send(documento);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.HojaDeVidaDocumentos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaDocumentos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearHojaDeVidaDocumentoRequest documento)
        {
            var resultado = await mediador.Send(documento);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.HojaDeVidaDocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaDocumentos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var documento = new EliminarHojaDeVidaDocumentoRequest { Id = id };
            var resultado = await mediador.Send(documento);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.HojaDeVidaDocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaDocumentos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialHojaDeVidaDocumentoRequest documento)
        {
            if (id != documento.Id) return BadRequest();

            var resultado = await mediador.Send(documento);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
