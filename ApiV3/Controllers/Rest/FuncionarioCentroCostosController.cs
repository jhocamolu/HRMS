using ApiV3.Dominio.FuncionarioCentroCostos.Comandos.Crear;
using ApiV3.Dominio.FuncionarioCentroCostos.Comandos.CrearManual;
using ApiV3.Dominio.FuncionarioCentroCostos.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU111
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioCentroCostosController : ControllerBase
    {
        private readonly IMediator mediador;

        public FuncionarioCentroCostosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: api/ActividadFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioCentroCostos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearFuncionarioCentroCostoRequest comando)
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

        #region POST CREAE MANUAL
        // POST: api/FuncionarioCentroCosto
        //PERMISOS   [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioCentroCostos_CrearManual })]
        [HttpPost("CrearManual")]
        public async Task<ActionResult> CrearnAMUAL([FromBody] CrearManualFuncionarioCentroCostoRequest comando)
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

        #region DELETE - Limpiar
        //delete: ApiV3.FuncionarioCentroCosto/lLimpiar/4
        // POST: api/ActividadFuncionarios
//        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioCentroCostos_Eliminar })]
        [HttpDelete("Limpiar/{FuncionarioId}")]
        public async Task<ActionResult> Eliminar(int funcionarioId)
        {
            var funcionarioCentroCosto = new EliminarFuncionarioCentroCostoRequest { FuncionarioId = funcionarioId };
            var resultado = await mediador.Send(funcionarioCentroCosto);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion
    }
}