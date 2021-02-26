using ApiV3.Dominio.RequisicionPersonales.Actualizar;
using ApiV3.Dominio.RequisicionPersonales.Crear;
using ApiV3.Dominio.RequisicionPersonales.Eliminar;
using ApiV3.Dominio.RequisicionPersonales.Estado;
using ApiV3.Dominio.RequisicionPersonales.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisicionPersonalesController : ControllerBase
    {
        private readonly IMediator mediador;
        public RequisicionPersonalesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.RequisicionPersonales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RequisicionPersonales_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarRequisicionPersonalRequest requisicion)
        {
            if (id != requisicion.Id) return BadRequest();
            var resultado = await mediador.Send(requisicion);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: RequisicionPersonales.formaPagos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RequisicionPersonales_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearRequisicionPersonalRequest requisicion)
        {
            var resultado = await mediador.Send(requisicion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.RequisicionPersonales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RequisicionPersonales_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var requisicion = new EliminarRequisicionPersonalRequest { Id = id };
            var resultado = await mediador.Send(requisicion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.RequisicionPersonales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RequisicionPersonales_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialRequisicionPersonalRequest requisicion)
        {
            if (id != requisicion.Id) return BadRequest();

            var resultado = await mediador.Send(requisicion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH ESTADO
        //PATCH: ApiV3.RequisicionPersonales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RequisicionPersonales_CambiarEstado })]
        [HttpPatch("{id}/Estado")]
        public async Task<ActionResult> Estado(int id, [FromBody] EstadoRequisicionPersonalRequest requisicion)
        {
            if (id != requisicion.Id) return BadRequest();

            var resultado = await mediador.Send(requisicion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
