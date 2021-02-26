using ApiV3.Dominio.Dashboard.Comandos.GraficasMovil;
using ApiV3.Dominio.Dashboard.Comandos.GraficasWeb;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU003 Dashboard 
/// Controler para generar optener datos del dashboard

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IMediator mediador;

        public DashboardsController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Dashboards_GraficasWeb })]
        [HttpPost("GraficasWeb")]
        public async Task<IActionResult> GraficasWeb([FromBody] GraficasWebDashboardRequest grafica)
        {
            var resultado = await mediador.Send(grafica);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Dashboards_GraficasMovil })]
        [HttpPost("GraficasMovil/{Id}")]
        public async Task<IActionResult> GraficasMovil(int id)
        {
            var grafica = new GraficasMovilDashboardRequest() { FuncionarioId = id };

            var resultado = await mediador.Send(grafica);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
    }
}
