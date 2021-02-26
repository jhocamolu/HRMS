using ApiV3.Dominio.DiagnosticoCies.Comandos.Actualizar;
using ApiV3.Dominio.DiagnosticoCies.Comandos.Crear;
using ApiV3.Dominio.DiagnosticoCies.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU017_Administrar_CIE10
/// Controlador 

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticoCiesController : ControllerBase
    {

        private readonly IMediator mediador;
        public DiagnosticoCiesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        // PUT: ApiV3.DiagnosticoCie/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DiagnosticoCies_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualiza(int id, [FromBody] ActualizarDiagnosticoCieRequest diagnosticoCie)
        {
            if (id != diagnosticoCie.Id) return BadRequest();
            var resultado = await this.mediador.Send(diagnosticoCie);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region POST
        //POST: ApiV3.DiagnosticoCie
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DiagnosticoCies_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearDiagnosticoCieRequest diagnosticoCie)
        {
            var resultado = await this.mediador.Send(diagnosticoCie);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //// DELETE: ApiV3.DiagnosticoCies/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var diagnosticoCie = new EliminarDiagnosticoCieRequest { Id = id };
        //    var resultado = await mediador.Send(diagnosticoCie);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        //}
        #endregion

        #region PATCH
        // PATCH: ApiV3.DiagnosticoCie/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DiagnosticoCies_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialDiagnosticoCieRequest diagnosticoCie)
        {
            if (id != diagnosticoCie.Id) return BadRequest();
            var resultado = await mediador.Send(diagnosticoCie);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { messagge = resultado.Data });
        }
        #endregion
    }
}
