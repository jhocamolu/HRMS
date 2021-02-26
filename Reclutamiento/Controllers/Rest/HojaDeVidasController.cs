using Reclutamiento.Dominio.HojaDeVidas.Comandos.Actualizar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Reclutamiento.Dominio.HojaDeVidas.Comandos.Parcial;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU099 controlador  HojaDeVidas

namespace Reclutamiento.Controllers.Rest
{
    [Route("reclutamiento/[controller]")]
    [ApiController]
    public class HojaDeVidasController : ControllerBase
    {
        private readonly IMediator mediador;
        public HojaDeVidasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.HojaDeVidas/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaRequest informacionHojaDeVida)
        {
            if (id != informacionHojaDeVida.Id) return BadRequest();
            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.HojaDeVidas/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialHojaDeVidaRequest informacionHojaDeVida)
        {
            if (id != informacionHojaDeVida.Id) return BadRequest();

            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
