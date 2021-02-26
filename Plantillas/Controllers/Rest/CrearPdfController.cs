using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Dominio.CrearPdf.Comando.CrearPdf;
using System.Threading.Tasks;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrearPdfController : ControllerBase
    {
        private readonly IMediator mediador;

        public CrearPdfController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // GET: api/CrearPdf
        [HttpGet("documento/{documentoSlug}/informacion/{informacion}")]
        public async Task<ActionResult<dynamic>> Get(string documentoSlug, int informacion)
        {
            var comando = new CrearPdfRequest
            {
                DocumentoSlug = documentoSlug,
                Id = informacion
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
