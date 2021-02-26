using ApiV3.Dominio.Funcionarios.Comandos.Actualizar;
using ApiV3.Dominio.Funcionarios.Comandos.Crear;
using ApiV3.Dominio.Funcionarios.Comandos.Parcial;
using ApiV3.Dominio.Funcionarios.Consulta;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  -------------------------------------
/// Controlador

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IMediator mediador;
        public FuncionariosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.formaPago/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Funcionarios_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarFuncionarioRequest funcionario)
        {
            if (id != funcionario.Id) return BadRequest();
            var resultado = await mediador.Send(funcionario);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.formaPagos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Funcionarios_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearFuncionarioRequest funcionario)
        {
            var resultado = await mediador.Send(funcionario);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.funcionario/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var funcionario = new EliminarFuncionarioRequest { Id = id };
        //    var resultado = await mediador.Send(funcionario);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        //}
        #endregion

        #region PATCH
        //PATCH: ApiV3.funcionario/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialFuncionarioRequest funcionario)
        {
            if (id != funcionario.Id) return BadRequest();

            var resultado = await mediador.Send(funcionario);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        #region GET
        //Busca los valores actiuales del contrato(contrato/OtroSi)
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Funcionarios_ObtenerDatosActuales })]
        [HttpGet("{FuncionarioId}/DatosActuales")]
        public async Task<ActionResult<object>> Obtener(int FuncionarioId)
        {
            var datosActuales = new ObtenerFuncionarioDatosActualesIdRequest()
            {
                FuncionarioId = FuncionarioId,
            };
            var resultado = await mediador.Send(datosActuales);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion
        #region GET
        //Busca el PDF del contrato del funcionario
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Funcionarios_ObtenerPDFContrato })]
        [HttpGet("PDFContrato/{id}")]
        public async Task<ActionResult> ObtenerPDFContrato(int? id)
        {
            var command = new ObtenerPDFContratoFuncionarioRequest()
            {
                FuncionarioId = id
            };
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion
    }
}