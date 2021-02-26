using ApiV3.Dominio.Reportes.Comandos.NovedadLibranza;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Reporte
{
    [Route("reporte/[controller]")]
    [ApiController]
    public class NovedadesLibranzaController : ControllerBase
    {
        private readonly IMediator mediador;

        public NovedadesLibranzaController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: reporte/NovedadesEmbargo
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NovedadesLibranza_GenerarReporte })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] ReporteNovedadLibranzaRequest link)
        {
            var resultado = await mediador.Send(link);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
    }
}