using ApiV3.Dominio.DeduccionRetefuentes.Comandos.Actualizar;
using ApiV3.Dominio.DeduccionRetefuentes.Comandos.Crear;
using ApiV3.Dominio.DeduccionRetefuentes.Comandos.Eliminar;
using ApiV3.Dominio.DeduccionRetefuentes.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU003 Dashboard 
/// Controler para generar optener datos del dashboard

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeduccionRetefuentesController : ControllerBase
    {
        private readonly IMediator mediador;
        public DeduccionRetefuentesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/DeduccionRetefuente/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DeduccionRetefuentes_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarDeduccionRetefuenteRequest deduccion)
        {
            if (id != deduccion.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(deduccion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region POST
        // POST: api/DeduccionRetefuente
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DeduccionRetefuentes_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearDeduccionRetefuenteRequest deduccion)
        {
            var resultado = await mediador.Send(deduccion);
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

        #region PATCH
        // PATCH: api/DeduccionRetefuente/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DeduccionRetefuentes_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialDeduccionRetefuenteRequest deduccion)
        {
            if (id != deduccion.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(deduccion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region DELETE
        // DELETE: api/DeduccionRetefuente/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DeduccionRetefuentes_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var deduccion = new EliminarDeduccionRetefuenteRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(deduccion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
    }
}
