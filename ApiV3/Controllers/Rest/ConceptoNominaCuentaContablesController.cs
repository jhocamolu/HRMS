using ApiV3.Dominio.ConceptoNominaCuentaContables.Actualizar;
using ApiV3.Dominio.ConceptoNominaCuentaContables.Crear;
using ApiV3.Dominio.ConceptoNominaCuentaContables.Eliminar;
using ApiV3.Dominio.ConceptoNominaCuentaContables.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    /// @author Jesus Albeiro Gaviria R
    /// @email  desarrollador5@alcanosesp.com
    /// @Description HU045_administrar_conceptos_nomina

    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoNominaCuentaContablesController : ControllerBase
    {
        private readonly IMediator mediador;
        public ConceptoNominaCuentaContablesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/[controller]onceptoNominaCuentaDebito/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaCuentaContables_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarConceptoNominaCuentaContableRequest cuentaDebito)
        {
            if (id != cuentaDebito.Id) return BadRequest();
            var resultado = await mediador.Send(cuentaDebito);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: api/[controller]onceptoNominaCuentaDebito
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaCuentaContables_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearConceptoNominaCuentaContableRequest cuentaDebito)
        {
            var resultado = await mediador.Send(cuentaDebito);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: api/[controller]onceptoNominaCuentaDebito/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaCuentaContables_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var cuentaDebito = new EliminarConceptoNominaCuentaContableRequest { Id = id };
            var resultado = await mediador.Send(cuentaDebito);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: api/[controller]onceptoNominaCuentaDebito/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaCuentaContables_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialConceptoNominaCuentaContableRequest cuentaDebito)
        {
            if (id != cuentaDebito.Id) return BadRequest();

            var resultado = await mediador.Send(cuentaDebito);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}