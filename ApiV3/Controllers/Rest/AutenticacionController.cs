using ApiV3.Dominio.Autenticacion.Comandos.Login;
using ApiV3.Dominio.Autenticacion.Comandos.LoginAplicacion;
using ApiV3.Dominio.Autenticacion.Comandos.Loguot;
using ApiV3.Dominio.Autenticacion.Comandos.PermisoAplicacion;
using ApiV3.Dominio.Autenticacion.Comandos.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IMediator mediador;

        public AutenticacionController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        // POST: ApiV3.Autenticacion/Login
        [HttpPost("Login")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult> Login(LoginCommand comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }


        // POST: ApiV3.Autenticacion/Login
        [HttpPost("LoginAplicacion")]
        public async Task<ActionResult> LoginAplicacion(LoginAplicacionCommand comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }

        // POST: ApiV3.Autenticacion/Refresh
        [HttpPost("Refresh")]
        public async Task<ActionResult> Refresh(RefreshTokenRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }


        // GET: ApiV3.Autenticacion/Logout
        [HttpGet("Logout")]
        public async Task<ActionResult> Logout([FromHeader] string JwtToken)
        {
            LogoutCommand command = new LogoutCommand
            {
                JwtToken = JwtToken
            };
            var result = await mediador.Send(command);

            return Ok(result);

        }

        // POST: ApiV3.Autenticacion/PermisoAplicacion
        [HttpPost("PermisoAplicacion")]
        public async Task<ActionResult> PermisoAplicacion(PermisoAplicacionCommand comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });

        }
    }
}