using ApiV3.Dominio.Reportes.Comandos.LibroVacaciones;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Reporte
{
    [Route("reporte/[controller]")]
    [ApiController]
    public class LibroVacacionesController : ControllerBase
    {
        private readonly IMediator mediador;

        public LibroVacacionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: reporte/NovedadesEmbargo
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibroVacaciones_GenerarReporte })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] ReporteLibroVacacionesRequest link)
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