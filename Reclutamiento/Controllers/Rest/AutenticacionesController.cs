using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reclutamiento.Dominio.Autenticacion.Login;
using Reclutamiento.Dominio.Autenticacion.RecordarClave;
using Reclutamiento.Dominio.Autenticacion.Registro;
using System.Threading.Tasks;

namespace Reclutamiento.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionesController : ControllerBase
    {
        private readonly IMediator mediador;

        public AutenticacionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        
        [HttpPost("Crear")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult> Crear([FromBody] RegistroAutenticacionRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }

        [HttpPost("Login")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult> Login([FromBody] LoginAutenticacionRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }

        [HttpPost("RecordarClave")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult> RecordarClave([FromBody] RecordarClaveRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }

        #endregion

    }
}
