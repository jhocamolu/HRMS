using ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Actualizar;
using ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Crear;
using ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Eliminar;
using ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class HojaDeVidaExperienciaLaboralesController : ControllerBase
    {
        private readonly IMediator mediador;
        public HojaDeVidaExperienciaLaboralesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.HojaDeVidas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaExperienciaLaborales_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaExperienciaLaboralRequest experiencia)
        {
            if (id != experiencia.Id) return BadRequest();
            var resultado = await mediador.Send(experiencia);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.HojaDeVidas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaExperienciaLaborales_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearHojaDeVidaExperienciaLaboralRequest experiencia)
        {
            var resultado = await mediador.Send(experiencia);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.HojaDeVidas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaExperienciaLaborales_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var informacionHojaDeVida = new EliminarHojaDeVidaExperienciaLaboralRequest { Id = id };
            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.HojaDeVidas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaExperienciaLaborales_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialHojaDeVidaExperienciaLaboralRequest experiencia)
        {
            if (id != experiencia.Id) return BadRequest();

            var resultado = await mediador.Send(experiencia);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
