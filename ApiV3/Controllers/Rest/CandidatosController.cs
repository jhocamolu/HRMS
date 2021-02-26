using ApiV3.Dominio.Candidatos.Comandos.Crear;
using ApiV3.Dominio.Candidatos.Comandos.Eliminar;
using ApiV3.Dominio.Candidatos.Comandos.Estado;
using ApiV3.Dominio.Candidatos.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R.
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU095_Reclutamiento_Selección_Personal


namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Candidatos_Crear })]
        [HttpPost]
        public async Task<ActionResult<Candidato>> Crear(CrearCandidatoRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        //#region PUT
        //// PUT: ApiV3.Candidato/5
        //[TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Candidatos_Actualizar })]
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarCandidatoRequest comando)
        //{
        //    if (id != comando.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var resultado = await mediador.Send(comando);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode(500, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        //#endregion

        #region DELETE
        // DELETE: ApiV3.Candidatos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Candidatos_Eliminar })]
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

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Candidatos_CambiarEstadoRegistro })]
        // PATCH: ApiV3.Candidatos/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialCandidatoRequest comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region PATCH ESTADO
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Candidatos_CambiarEstado })]
        //PATCH: Api/Candidatos/5/Estado
        [HttpPatch("{id}/Estado")]
        public async Task<ActionResult> Estado(int id, [FromBody] EstadoCandidatoRequest estado)
        {
            if (id != estado.Id) return BadRequest();

            var resultado = await mediador.Send(estado);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
