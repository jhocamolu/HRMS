using ApiV3.Dominio.FormaPagos.Actualizar;
using ApiV3.Dominio.FormaPagos.Crear;
using ApiV3.Dominio.FormaPagos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU020_Administrar_formas_pago
/// Controlador

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaPagosController : ControllerBase
    {
        private readonly IMediator mediador;
        public FormaPagosController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region PUT
        //PUT: ApiV3.formaPago/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FormaPagos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarFormaPagoRequest formaPago)
        {
            if (id != formaPago.Id) return BadRequest();
            var resultado = await mediador.Send(formaPago);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.formaPagos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FormaPagos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearFormaPagoRequest formaPago)
        {
            var resultado = await mediador.Send(formaPago);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        ////delete: ApiV3.formaPagos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> eliminar(int id)
        //{
        //    var formaPago = new EliminarFormaPagoRequest { Id = id };
        //    var resultado = await mediador.Send(formaPago);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        //}
        #endregion

        #region PATCH
        //PATCH: ApiV3.formaPagos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FormaPagos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialFormaPagoRequest formaPago)
        {
            if (id != formaPago.Id) return BadRequest();

            var resultado = await mediador.Send(formaPago);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
