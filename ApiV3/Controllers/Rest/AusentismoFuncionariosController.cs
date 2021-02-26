using ApiV3.Dominio.AusentismoFuncionarios.Comandos.Actualizar;
using ApiV3.Dominio.AusentismoFuncionarios.Comandos.Crear;
using ApiV3.Dominio.AusentismoFuncionarios.Comandos.Eliminar;
using ApiV3.Dominio.AusentismoFuncionarios.Comandos.Estado;
using ApiV3.Dominio.AusentismoFuncionarios.Comandos.Parcial;
using ApiV3.Dominio.AusentismoFuncionarios.TareaProgramada.Finalizar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU048_Ausentismo_Funcionario
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class AusentismoFuncionariosController : ControllerBase
    {
        private readonly IMediator mediador;

        public AusentismoFuncionariosController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region POST        
        // POST: ApiV3.AusentismoFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_Crear })]
        [HttpPost]
        public async Task<ActionResult<AusentismoFuncionario>> Crear(CrearAusentismoFuncionarioRequest comando)
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
        // PUT: ApiV3.AusentismoFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarAusentismoFuncionarioRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_CambiarEstadoRegistro })]
        // PATCH: ApiV3.AusentismoFuncionarios/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialAusentismoFuncionarioRequest comando)
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

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_CambiarEstado })]
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoAusentismoFuncionarioRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_TareaProgramadaFinalizar })]
        //PATCH: Api/AusentismoFuncionarios/TareaProgramadaFinalizar
        [HttpPatch("TareaProgramadaFinalizar")]
        public async Task<ActionResult> TareaProgramadaFinalizar([FromBody] FinalizarAusentismoRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        // DELETE: ApiV3.AusentismoFuncionario/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AusentismoFuncionarios_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<AusentismoFuncionario>> Eliminar(int id)
        {
            var comando = new EliminarAusentismoFuncionarioRequest
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
