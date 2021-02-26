using ApiV3.Dominio.Reportes.Comandos.AusentismoLaborales;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Reporte
{
    [Route("reporte/[controller]")]
    [ApiController]
    public class AusentismoLaboralController : ControllerBase
    {
        private readonly IMediator mediador;

        public AusentismoLaboralController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: api/BeneficioCorporativos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoLaboral_GenerarReporte })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] ReporteAusentismoLaboralRequest link)
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
