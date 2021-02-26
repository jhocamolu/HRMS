using ApiV3.Dominio.ContratoCentroTrabajos.Comandos.Actualizar;
using ApiV3.Dominio.ContratoCentroTrabajos.Comandos.Crear;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU112_Administrar_ContratoCentroTrabajo

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoCentroTrabajosController : ControllerBase
    {
        private readonly IMediator mediador;

        public ContratoCentroTrabajosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/ContratoCentroTrabajo/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoCentroTrabajos_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarContratoCentroTrabajoRequest contrato)
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
        // POST: api/CrearContratoCentroTrabajos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoCentroTrabajos_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearContratoCentroTrabajoRequest contratos)
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
    }
}