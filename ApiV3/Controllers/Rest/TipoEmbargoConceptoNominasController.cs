using ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Actualizar;
using ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Crear;
using ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU051_Tipo_Embargo

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEmbargoConceptoNominasController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoEmbargoConceptoNominasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/TipoEmbargosConceptoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargoConceptoNominas_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTipoEmbargoConceptoNominaRequest comando)
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
        // PUT: api/TipoEmbargosConceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargoConceptoNominas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarTipoEmbargoConceptoNominaRequest comando)
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
        // DELETE: api/TipoEmbargosConceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargoConceptoNominas_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarTipoEmbargoConceptoNominaRequest
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
