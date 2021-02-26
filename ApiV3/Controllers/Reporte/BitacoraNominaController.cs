using ApiV3.Dominio.Reportes.Comandos.BitacoraNomina;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Reporte
{
    [Route("reporte/[controller]")]
    [ApiController]
    public class BitacoraNominaController : ControllerBase
    {
        private readonly IMediator mediador;

        public BitacoraNominaController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: api/BitacoraNomina
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.BitacoraNomina_GenerarReporte })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] ReporteBitacoraNominaRequest link)
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
