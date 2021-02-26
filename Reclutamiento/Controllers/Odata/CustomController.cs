using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reclutamiento.Dominio.Custom.Comandos;
using Reclutamiento.Dominio.Dashboard.Comandos.DashboarPortal;
using System.Threading.Tasks;

namespace Reclutamiento.Controllers.Rest
{
    [Route("odata/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        private readonly IMediator mediador;
        public CustomController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        [HttpGet("{argumentos}")]
        public async Task<ActionResult> Consultar()
        {
            var comando = new CustomRequest();
            
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }


        [HttpGet("DashboardPortal/{Id}")]
        public async Task<IActionResult> DashboarPortal(string id)
        {
            var grafica = new DashboardPortalRequest() { NumeroDocumento = id };

            var resultado = await mediador.Send(grafica);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
    }
}
