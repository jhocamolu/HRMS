using ApiV3.Dominio.Reportes.Comandos.Registraduria;
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
    public class RegistraduriaController : ControllerBase
    {
        private readonly IMediator mediador;

        public RegistraduriaController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: api/ReporteRegistradurias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Registraduria_GenerarReporte })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] ReporteRegistraduriaRequest link)
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
