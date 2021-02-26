using ApiV3.Dominio.Nominas.Comandos.Actualizar;
using ApiV3.Dominio.Nominas.Comandos.Aplicar;
using ApiV3.Dominio.Nominas.Comandos.Aprobar;
using ApiV3.Dominio.Nominas.Comandos.Crear;
using ApiV3.Dominio.Nominas.Comandos.Graficas;
using ApiV3.Dominio.Nominas.Comandos.Parcial;
using ApiV3.Dominio.Nominas.Consultas.ConsultaCabecera;
using ApiV3.Dominio.Nominas.Consultas.PeriodoContableActivo;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU052_registrar_informacion_básica_liquidación_nomina
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominasController : ControllerBase
    {
        private readonly IMediator mediador;

        public NominasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/Nominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_Crear })]
        [HttpPost]
        public async Task<ActionResult<Nomina>> Crear([FromBody] CrearNominaRequest command)
        {
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region PUT
        // PUT: api/Nominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarNominaRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialNominaRequest comando)
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

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_CambiarEstadoRegistro })]
        [HttpPatch("aprobar/{id}")]
        public async Task<IActionResult> Aprobar(int id, [FromBody] AprobarNominaRequest comando)
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

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_CambiarEstadoRegistro })]
        [HttpPatch("aplicar/{id}")]
        public async Task<IActionResult> Aplicar(int id, [FromBody] AplicarNominaRequest comando)
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

        #region GET
        //Busca el período contable activo
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_ObtenerPeriodoContableActivo })]
        [HttpGet("PeriodoContableActivo")]
        public async Task<ActionResult<object>> Obtener(int i)
        {
            var command = new ObtenerPeriodoContableActivoRequest();

            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_ObtenerGraficas })]
        [HttpGet("Graficas/{id}")]
        public async Task<IActionResult> Graficas(int id)
        {
            var command = new GraficasNominaRequest()
            {
                Id = id
            };

            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_ObtenerNominaCabecera })]
        [HttpGet("NominaCabecera/{id}")]
        public async Task<IActionResult> Cabecera(int id)
        {
            var command = new ConsultaNominaRequest()
            {
                NominaId = id
            };

            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Ocupacion>> Eliminar(int id)
        //{
        //    var command = new EliminarNominaRequest
        //    {
        //        Id = id
        //    };
        //    var resultado = await mediador.Send(command);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode(500, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        #endregion
    }
}
