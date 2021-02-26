using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiV3.Dominio.TipoLiquidacionComprobantes.Comandos.Actualizar;
using ApiV3.Dominio.TipoLiquidacionComprobantes.Comandos.Crear;
using ApiV3.Dominio.TipoLiquidacionComprobantes.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLiquidacionComprobantesController : ControllerBase
    {
        private readonly IMediator mediador;

        public TipoLiquidacionComprobantesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/ipoLiquidacionComprobantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionComprobantes_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearTipoLiquidacionComprobanteRequest comando)
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
        // PUT: api/ipoLiquidacionComprobantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionComprobantes_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarTipoLiquidacionComprobanteRequest comando)
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
        //DELETE: api/ipoLiquidacionComprobantes/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionComprobantes_Eliminar })]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarTipoLiquidacionComprobanteRequest
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