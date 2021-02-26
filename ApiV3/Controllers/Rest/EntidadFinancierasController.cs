using ApiV3.Dominio.EntidadesFinancieras.Comandos.Actualizar;
using ApiV3.Dominio.EntidadesFinancieras.Comandos.Crear;
using ApiV3.Dominio.EntidadFinancieras.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU012_Administrar_Entidades_Financieras

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadFinancierasController : ControllerBase
    {
        private readonly IMediator mediador;
        public EntidadFinancierasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/[controller]ntidadFinancieras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EntidadFinancieras_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearEntidadFinancieraRequest comando)
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

        #region PUT
        // PUT: api/[controller]ntidadFinancieras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EntidadFinancieras_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarEntidadFinancieraRequest comando)
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

        #region DELETE
        // DELETE: api/[controller]ntidadFinancieras/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var comando = new EliminarEntidadFinancieraRequest
        //    {
        //        Id = id
        //    };
        //    var resultado = await mediador.Send(comando);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        #endregion

        #region PATCH
        // PATCH: api/[controller]ntidadFinancieras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EntidadFinancieras_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialEntidadFinancieraRequest comando)
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
    }
}
