using ApiV3.Dominio.Reportes.Comandos.DesprendiblePagos;
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
    public class DesprendiblePagosController : ControllerBase
    {
        private readonly IMediator mediador;

        public DesprendiblePagosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        //POST: ApiV3.DiagnosticoCie
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DesprendiblePagos_GenerarReporte })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] ReporteDesprendiblePagoRequest desprendible)
        {
            var resultado = await this.mediador.Send(desprendible);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}