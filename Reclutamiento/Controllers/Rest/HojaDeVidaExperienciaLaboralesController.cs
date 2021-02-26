using Reclutamiento.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Actualizar;
using Reclutamiento.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Crear;
using Reclutamiento.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Eliminar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Controllers.Rest
{
    [Route("reclutamiento/[controller]")]
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
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaExperienciaLaboralRequest experiencia)
        {
            if (id != experiencia.Id) return BadRequest();
            var resultado = await mediador.Send(experiencia);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.HojaDeVidas
        
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
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var informacionHojaDeVida = new EliminarHojaDeVidaExperienciaLaboralRequest { Id = id };
            var resultado = await mediador.Send(informacionHojaDeVida);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

    }
}
