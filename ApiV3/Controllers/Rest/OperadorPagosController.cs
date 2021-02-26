using ApiV3.Dominio.OperadorPagos.Comando.Actualizar;
using ApiV3.Dominio.OperadorPagos.Comando.Crear;
using ApiV3.Dominio.OperadorPagos.Comando.Eliminar;
using ApiV3.Dominio.OperadorPagos.Comando.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU022_Administrar_Operadores_Pago
/// Controlador API

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadorPagosController : ControllerBase
    {
        private readonly IMediator mediador;
        public OperadorPagosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        // put: api/[controller]ipodocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.OperadorPagos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarOperadorPagoRequest operadorPago)
        {
            if (id != operadorPago.Id) return BadRequest();

            var actualizar = await mediador.Send(operadorPago);
            if (actualizar.IsSuccess) return Ok(actualizar.Data);
            return StatusCode(500, new { Message = actualizar.FailureReason });
        }
        #endregion

        #region POST
        // POST: api/[controller]ipoDocumentos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.OperadorPagos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearOperadorPagoRequest operadorPago)
        {
            var resultado = await mediador.Send(operadorPago);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        // DELETE: api/[controller]peradorPagos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.OperadorPagos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var operadorPago = new EliminarOperadorPagoRequest { Id = id };

            var resultado = await mediador.Send(operadorPago);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH api/[controller]ipoDocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.OperadorPagos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialOperadorPagoRequest tipodocumento)
        {
            if (id != tipodocumento.Id) return BadRequest();

            var resultado = await mediador.Send(tipodocumento);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion
    }
}
