using ApiV3.Dominio.Novedades.Cargar;
using ApiV3.Dominio.Novedades.Comandos.Actualizar;
using ApiV3.Dominio.Novedades.Comandos.Crear;
using ApiV3.Dominio.Novedades.Comandos.Eliminar;
using ApiV3.Dominio.Novedades.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU082_Otras Novedades
/// 
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadesController : ControllerBase
    {
        private readonly IMediator mediador;

        public NovedadesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        // PUT: api/[controller]cupaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Novedades_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarNovedadRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region POST
        // POST: api/[controller]cupaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Novedades_Crear })]
        [HttpPost]
        public async Task<ActionResult<Novedad>> Crear([FromBody] CrearNovedadRequest command)
        {
            var resultado = await mediador.Send(command);
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

        #region POST
        // POST: api/Cargar
        [HttpPost("cargar")]
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Novedades_Cargar })]
        public async Task<ActionResult<Novedad>> Cargar([FromForm]CargarNovedadRequest command)
        {
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess)
            {
                if (resultado.Data.ArchivoErrores == true)
                {
                    return File(resultado.Data.stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultado.Data.excelName);
                }
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
        
        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Novedades_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialNovedadRequest comando)
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
        // DELETE: api/[controller]cupaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Novedades_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Novedad>> Eliminar(int id)
        {
            var command = new EliminarNovedadRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
    }
}
