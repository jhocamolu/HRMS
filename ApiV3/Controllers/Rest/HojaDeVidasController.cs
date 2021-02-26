using ApiV3.Dominio.HojaDeVidas.Comandos.Actualizar;
using ApiV3.Dominio.HojaDeVidas.Comandos.Crear;
using ApiV3.Dominio.HojaDeVidas.Comandos.Eliminar;
using ApiV3.Dominio.HojaDeVidas.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU099 controlador  HojaDeVidas

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaRequest informacionHojaDeVida)
        {
            if (id != informacionHojaDeVida.Id) return BadRequest();
            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.HojaDeVidas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidas_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearHojaDeVidaRequest informacionHojaDeVida)
        {
            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.HojaDeVidas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidas_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var informacionHojaDeVida = new EliminarHojaDeVidaRequest { Id = id };
            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.HojaDeVidas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidas_CambiarEstadoRegistro })]
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
