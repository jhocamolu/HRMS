using Reclutamiento.Dominio.HojaDeVidaDocumentos.Comandos.Actualizar;
using Reclutamiento.Dominio.HojaDeVidaDocumentos.Comandos.Crear;
using Reclutamiento.Dominio.HojaDeVidaDocumentos.Comandos.Eliminar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU099 controlador  HojaDeVidas

namespace Reclutamiento.Controllers.Rest
{
    [Route("reclutamiento/[controller]")]
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
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaDocumentoRequest documento)
        {
            if (id != documento.Id) return BadRequest();
            var resultado = await mediador.Send(documento);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.HojaDeVidaDocumentos
        
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
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var documento = new EliminarHojaDeVidaDocumentoRequest { Id = id };
            var resultado = await mediador.Send(documento);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
