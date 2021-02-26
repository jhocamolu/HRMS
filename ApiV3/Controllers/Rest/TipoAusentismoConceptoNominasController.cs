using ApiV3.Dominio.TipoAusentismoConceptoNominas.Comandos.Actualizar;
using ApiV3.Dominio.TipoAusentismoConceptoNominas.Comandos.Crear;
using ApiV3.Dominio.TipoAusentismoConceptoNominas.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU047_AdministrarTipoAusentismoConceptoNomina
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAusentismoConceptoNominasController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoAusentismoConceptoNominasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // PUT: api/TipoAusentismoConceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAusentismoConceptoNominas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarTipoAusentismoConceptoNominaRequest actualizarConcepto)
        {
            if (id != actualizarConcepto.Id) return BadRequest();
            var resultado = await mediador.Send(actualizarConcepto);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }

        // POST: api/TipoAusentismoConceptoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAusentismoConceptoNominas_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTipoAusentismoConceptoNominaRequest comando)
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

        // DELETE: api/TipoAusentismoConceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAusentismoConceptoNominas_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminarConcepto = new EliminarTipoAusentismoConceptoNominaRequest { Id = id };
            var resultado = await mediador.Send(eliminarConcepto);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });

        }
    }
}
