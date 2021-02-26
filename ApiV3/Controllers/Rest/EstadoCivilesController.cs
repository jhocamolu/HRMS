using ApiV3.Dominio.EstadosCiviles.Comandos.Actualizar;
using ApiV3.Dominio.EstadosCiviles.Comandos.Crear;
using ApiV3.Dominio.EstadosCiviles.Comandos.Estado;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU003_Administrar_Estados_Civiles

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCivilesController : ControllerBase
    {
        private readonly IMediator mediador;

        public EstadoCivilesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EstadoCiviles_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarEstadoCivilRequest comando)
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

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EstadoCiviles_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoEstadoCivilRequest comando)
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

        #region POST
        // POST: api/[controller]stadoCiviles
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EstadoCiviles_Crear })]
        [HttpPost()]
        public async Task<ActionResult<EstadoCivil>> Crear([FromBody] CrearEstadoCivilRequest comando)
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

        //#region DELETE
        //// DELETE: api/[controller]stadoCiviles/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<EstadoCivil>> Eliminar(int id, EliminarEstadoCivilRequest comando)
        //{
        //    if (id != comando.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var resultado = await mediador.Send(comando);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode(500, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        //#endregion
    }
}
