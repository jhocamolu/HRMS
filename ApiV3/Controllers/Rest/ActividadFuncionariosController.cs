using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiV3.Dominio.ActividadFuncionarios.Comandos.Crear;
using ApiV3.Dominio.ActividadFuncionarios.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU111
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadFuncionariosController : ControllerBase
    {
        private readonly IMediator mediador;

        public ActividadFuncionariosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarios_Crear })]
        // POST: api/ActividadFuncionarios
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearActividadFuncionarioRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region DELETE
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadFuncionarios_Eliminar })]
        // DELETE: /ActividadFuncionario
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Calendarios_Eliminar })]
        [HttpDelete]
        public async Task<ActionResult<ActividadFuncionariosController>> Delete()
        {
            var comando = new EliminarActividadFuncionarioRequest();
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
