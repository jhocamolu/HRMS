using ApiV3.Dominio.Libranzas.Comandos.Actualizar;
using ApiV3.Dominio.Libranzas.Comandos.Crear;
using ApiV3.Dominio.Libranzas.Comandos.Estado;
using ApiV3.Dominio.Libranzas.TareaProgramada.IniciarVigencia;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU059_Administrar_Libranza
/// 
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibranzasController : ControllerBase
    {
        private readonly IMediator mediador;

        public LibranzasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST              
        // POST: api/Libranzas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Libranzas_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearLibranzaRequest comando)
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
        // PUT: api/Libranzas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Libranzas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarLibranzaRequest comando)
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
        /*// PATCH: api/Libranzas/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialLibranzaRequest contrato)
        {
            if (id != contrato.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(contrato);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }*/

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Libranzas_CambiarEstado })]
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoLibranzaRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Libranzas_TareaProgramadaIniciar })]
        //PATCH: Api/Libranzas/TareaProgramadaIniciar
        [HttpPatch("TareaProgramadaIniciar")]
        public async Task<ActionResult> TareaProgramadaIniciar([FromBody] IniciarVigenciaLibranzaRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var comando = new EliminarLibranzaRequest
        //    {
        //        Id = id
        //    };
        //    var resultado = await mediador.Send(comando);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        #endregion
    }
}
