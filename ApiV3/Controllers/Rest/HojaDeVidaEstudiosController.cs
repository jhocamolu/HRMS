using ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Actualizar;
using ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Crear;
using ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Eliminar;
using ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  controlador  HojaDeVidas

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class HojaDeVidaEstudiosController : ControllerBase
    {
        private readonly IMediator mediador;
        public HojaDeVidaEstudiosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.HojaDeVidaEstudios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaEstudios_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaEstudioRequest estudio)
        {
            if (id != estudio.Id) return BadRequest();
            var resultado = await mediador.Send(estudio);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.HojaDeVidaEstudios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaEstudios_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearHojaDeVidaEstudioRequest estudio)
        {
            var resultado = await mediador.Send(estudio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.HojaDeVidaEstudios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaEstudios_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var estudio = new EliminarHojaDeVidaEstudioRequest { Id = id };
            var resultado = await mediador.Send(estudio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.HojaDeVidaEstudios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaEstudios_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialHojaDeVidaEstudioRequest informacionHojaDeVida)
        {
            if (id != informacionHojaDeVida.Id) return BadRequest();

            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
