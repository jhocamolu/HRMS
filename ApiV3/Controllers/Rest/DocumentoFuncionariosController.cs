using ApiV3.Dominio.DocumentoFuncionarios.Comandos.Actualizar;
using ApiV3.Dominio.DocumentoFuncionarios.Comandos.Crear;
using ApiV3.Dominio.DocumentoFuncionarios.Comandos.Eliminar;
using ApiV3.Dominio.DocumentoFuncionarios.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    /// @author Jesus Albeiro Gaviria R
    /// @email  desarrollador5@alcanosesp.com
    /// @Description HU033_Administrar_Documentos_Funcionario

    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoFuncionariosController : ControllerBase
    {
        private readonly IMediator mediador;
        public DocumentoFuncionariosController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region PUT
        //PUT: ApiV3.DocumentoFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DocumentoFuncionarios_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarDocumentoFuncionarioRequest documentoFuncionario)
        {
            if (id != documentoFuncionario.Id) return BadRequest();
            var resultado = await mediador.Send(documentoFuncionario);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.DocumentoFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DocumentoFuncionarios_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearDocumentoFuncionarioRequest documentoFuncionario)
        {
            var resultado = await mediador.Send(documentoFuncionario);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.DocumentoFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DocumentoFuncionarios_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var command = new EliminarDocumentoFuncionarioRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.DocumentoFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DocumentoFuncionarios_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialDocumentoFuncionarioRequest comando)
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
    }
}
