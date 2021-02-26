using ApiV3.Dominio.Contratos.Comandos.Actualizar;
using ApiV3.Dominio.Contratos.Comandos.Crear;
using ApiV3.Dominio.Contratos.Comandos.Eliminar;
using ApiV3.Dominio.Contratos.Comandos.Finalizar;
using ApiV3.Dominio.Contratos.Comandos.Parcial;
using ApiV3.Dominio.Contratos.Consultas;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly IMediator mediador;
        public ContratosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/[controller]ontratos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Contratos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarContratoRequest contrato)
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
        }
        #endregion

        #region POST
        // POST: api/[controller]ontratos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Contratos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearContratosRequest contratos)
        {
            var resultado = await mediador.Send(contratos);
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
        // PATCH: api/[controller]ontrato/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Contratos_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialContratoRequest contrato)
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
        }

        // PATCH: api/Contrato/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Contratos_Finalizar })]
        [HttpPatch("finalizar/{id}")]
        public async Task<IActionResult> Finalizar(int id, [FromBody] FinalizarContratoRequest contrato)
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
        }

        // Patch: api/LibroVacaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Contrato_TareaProgramadaFinalizar })]
        [HttpPatch("TareaProgramadaFinalizar")]
        public async Task<ActionResult> TareaProgramadaFinalizar(Dominio.Contratos.TareasProgramadas.Finalizar.FinalizarContratoRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        // DELETE: api/[controller]entroTrabajos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoOtroSis_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarContratoRequest
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

        #region GET
        //Busca los valores actiuales del contrato(contrato/OtroSi)
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoOtroSis_Eliminar })]
        [HttpGet("{ContratoId}/DatosActuales")]
        public async Task<ActionResult<object>> Obtener(int ContratoId)
        {
            var otroSi = new ObtenerContratoDatosActualesRequest()
            {
                ContratoId = ContratoId,
            };
            var resultado = await mediador.Send(otroSi);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion
    }
}
