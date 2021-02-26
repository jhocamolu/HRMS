
using ApiV3.Dominio.CuantaBancaria.Comandos.Actualizar;
using ApiV3.Dominio.CuantaBancaria.Comandos.Crear;
using ApiV3.Dominio.CuantaBancaria.Comandos.Eliminar;
using ApiV3.Dominio.CuantaBancaria.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// Controlador

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaBancariasController : ControllerBase
    {
        private readonly IMediator mediador;


        public CuentaBancariasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaBancaria_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarCuentaBancariaRequest cuenta)
        {
            if (id != cuenta.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(cuenta);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: api/CuentaBancarias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaBancaria_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearCuentaBancariaRequest cuenta)
        {
            var resultado = await mediador.Send(cuenta);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaBancaria_CambiarEstadoRegistro })]
        // PATCH: api/[Beneficios/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialCuentaBancariaRequest cuenta)
        {
            if (id != cuenta.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(cuenta);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region DELETE
        // DELETE: api/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaBancaria_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var cuenta = new EliminarCuentaBancariaRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(cuenta);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
    }
}
