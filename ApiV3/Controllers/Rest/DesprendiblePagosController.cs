using ApiV3.Dominio.DesprendiblePagos.Consultas.ObtenerDesprendiblePago;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesprendiblePagosController : ControllerBase
    {

        private readonly IMediator mediador;

        public DesprendiblePagosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // GET: api/DesprendiblePagos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DesprendiblePagos_Listar })]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> Listar(int id)
        {
            var command = new ObtenerDesprendiblePagoRequest
            {
                FuncionarioId = id
            };
            var resultado = await mediador.Send(command);
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
