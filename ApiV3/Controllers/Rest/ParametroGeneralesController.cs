using ApiV3.Dominio.ParametroGenerales.Comandos.Parcial;
using ApiV3.Dominio.ParametroGenerales.Comandos.ParcialGrupo;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU049_Parametro_General
/// Controlador API

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroGeneralesController : ControllerBase
    {
        private readonly IMediator mediador;

        public ParametroGeneralesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PATCH Individual
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ParametroGenerales_Actualizar })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialParametroGeneralRequest comando)
        {

            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
        #region PATCH Muchos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ParametroGenerales_ActualizarGrupo })]
        [HttpPatch]
        public async Task<IActionResult> Parciales([FromBody] ParcialGrupoParametroGeneralRequest comando)
        {

            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
    }
}
