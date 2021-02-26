using ApiV3.Dominio.ConceptoNominas.Comandos.Actualizar;
using ApiV3.Dominio.ConceptoNominas.Comandos.Crear;
using ApiV3.Dominio.ConceptoNominas.Comandos.Eliminar;
using ApiV3.Dominio.ConceptoNominas.Comandos.Parcial;
using ApiV3.Dominio.ConceptoNominas.Comandos.Reordenar;
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
    public class ConceptoNominasController : ControllerBase
    {
        private readonly IMediator mediador;
        public ConceptoNominasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/[controller]onceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaCuentaContables_Eliminar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarConceptoNominaRequest actualizarConcepto)
        {
            if (id != actualizarConcepto.Id) return BadRequest();
            var resultado = await mediador.Send(actualizarConcepto);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: api/[controller]onceptoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominas_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearConceptoNominaRequest crearConcepto)
        {
            var resultado = await mediador.Send(crearConcepto);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: api/[controller]onceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominas_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminarConcepto = new EliminarConceptoNominaRequest { Id = id };
            var resultado = await mediador.Send(eliminarConcepto);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: api/[controller]onceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialConceptoNominaRequest parcialConcepto)
        {
            if (id != parcialConcepto.Id) return BadRequest();

            var resultado = await mediador.Send(parcialConcepto);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }

        //PATCH: api/[controller]onceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominas_Reordenar })]
        [HttpPatch("Reordenar/{id}")]
        public async Task<ActionResult> Reordenar(int id, [FromBody] ReordenarConceptoNominaRequest parcialConcepto)
        {
            if (id != parcialConcepto.Id) return BadRequest();

            var resultado = await mediador.Send(parcialConcepto);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
