using ApiV3.Dominio.CentroCostos.TareaProgramada;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description 
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroCostosController : ControllerBase
    {
        private readonly IMediator mediador;

        public CentroCostosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region GET
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroCostos_TareaProgramadaObtener })]
        [HttpGet("TareaProgramadaObtener")]
        public async Task<ActionResult> TareaProgramadaObtener([FromBody] ObtenerCentroCostoRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

    }
}
