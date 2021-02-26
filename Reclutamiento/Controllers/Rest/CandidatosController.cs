

using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reclutamiento.Dominio.Candidatos.Comandos.Crear;
using Reclutamiento.Dominio.Candidatos.Comandos.Eliminar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R.
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU095_Reclutamiento_Selección_Personal


namespace Reclutamiento.Controllers.Rest
{
    [Route("reclutamiento/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private readonly IMediator mediador;

        public CandidatosController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region POST        
        // POST: ApiV3.Candidatos
        
        [HttpPost]
        public async Task<ActionResult<Candidato>> Crear(CrearCandidatoRequest comando)
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

        #region DELETE
        // DELETE: ApiV3.Candidatos/5
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Candidato>> Eliminar(int id)
        {
            var comando = new EliminarCandidatoRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }

        #endregion

    }
}
