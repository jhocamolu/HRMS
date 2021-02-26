using ApiV3.Dominio.ContratoOtroSis.Comandos.Aumentar;
using ApiV3.Dominio.ContratoOtroSis.Comandos.Crear;
using ApiV3.Dominio.ContratoOtroSis.Comandos.Eliminar;
using ApiV3.Dominio.ContratoOtroSis.Consultas.Documento;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU039_Administrar_otrosi
/// Controlador
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoOtroSisController : ControllerBase
    {
        private readonly IMediator mediador;
        public ContratoOtroSisController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT 
        ////PUT: api/[controller]ontratoOtroSis/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarContratoOtroSiRequest actualizarOtroSi)
        //{
        //    if (id != actualizarOtroSi.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var resultado = await mediador.Send(actualizarOtroSi);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode(500, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        #endregion

        #region POST
        // POST: api/[controller]ontratoOtroSis
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoOtroSis_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearContratoOtroSiRequest crearOtroSi)
        {
            var resultado = await mediador.Send(crearOtroSi);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }

        // POST: api/[controller]ontratoOtroSis
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoOtroSis_Crear })]
        [HttpPost("Aumentar")]
        public async Task<ActionResult> Aumentar([FromBody] AumentarContratoOtroSiRequest crearOtroSi)
        {
            var resultado = await mediador.Send(crearOtroSi);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        ////PATCH: api/[controller]ontratoOtroSis/5
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> Parcial(int id, [FromBody] ParcialContratoOtroSiRequest parcialOtroSi)
        //{
        //    if (id != parcialOtroSi.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var resultado = await mediador.Send(parcialOtroSi);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode(500, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        #endregion

        #region DELETE
        // DELETE: api/[controller]ontratoOtroSis/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ContratoOtroSis_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var otroSi = new EliminarContratoOtroSiRequest { Id = id };
            var resultado = await mediador.Send(otroSi);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region GET
        //Busca el PDF del contrato del funcionario
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Funcionarios_ObtenerPDFContrato })]
        [HttpGet("PDFContrato/{id}")]
        public async Task<ActionResult> ObtenerPDF(int? id)
        {
            var command = new DocumentoContratoOtroSiRequest()
            {
                OtroSi = (int)id
            }
            ;
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion

    }
}
