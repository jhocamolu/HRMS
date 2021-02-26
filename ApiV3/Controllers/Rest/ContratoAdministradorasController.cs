using ApiV3.Dominio.ContratoAdministradoras.Comandos.Actualizar;
using ApiV3.Dominio.ContratoAdministradoras.Comandos.Crear;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU106
/// Controlador

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoAdministradorasController : ControllerBase
    {
        private readonly IMediator mediador;
        public ContratoAdministradorasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/CrearContratoAdministradoras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoAdministradoras_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarContratoAdministradoraRequest contrato)
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
        // POST: api/CrearContratoAdministradoras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoAdministradoras_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearContratoAdministradoraRequest contratos)
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
