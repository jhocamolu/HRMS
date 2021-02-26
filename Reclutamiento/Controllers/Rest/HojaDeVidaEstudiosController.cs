using Reclutamiento.Dominio.HojaDeVidaEstudios.Comandos.Actualizar;
using Reclutamiento.Dominio.HojaDeVidaEstudios.Comandos.Crear;
using Reclutamiento.Dominio.HojaDeVidaEstudios.Comandos.Eliminar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  controlador  HojaDeVidas

namespace Reclutamiento.Controllers.Rest
{
    [Route("reclutamiento/[controller]")]
    [ApiController]
    public class HojaDeVidaEstudiosController :ControllerBase
    {
        private readonly IMediator mediador;
        public HojaDeVidaEstudiosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.HojaDeVidaEstudios/5
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarHojaDeVidaEstudioRequest estudio)
        {
            if (id != estudio.Id) return BadRequest();
            var resultado = await mediador.Send(estudio);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.HojaDeVidaEstudios
        
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
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var estudio = new EliminarHojaDeVidaEstudioRequest { Id = id };
            var resultado = await mediador.Send(estudio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        
    }
}
