using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reportes.Dominio.GenerarReportes.Comando.Generar;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace Reportes.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerarReportesController : ControllerBase
    {
        private readonly IMediator mediador;

        public GenerarReportesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: api/GenerarReportes
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody]GenerarReporteRequest comando)
        {
            
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });

        }
    }
}
