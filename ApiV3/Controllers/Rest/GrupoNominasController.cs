using ApiV3.Dominio.GrupoNominas.Comandos.Actualizar;
using ApiV3.Dominio.GrupoNominas.Comandos.Crear;
using ApiV3.Dominio.GrupoNominas.Comandos.Eliminar;
using ApiV3.Dominio.GrupoNominas.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU044_Actividades_Economicas
/// 
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoNominasController : ControllerBase
    {
        private readonly IMediator mediador;

        public GrupoNominasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST
        // POST: ApiV3.GrupoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GrupoNominas_Crear })]
        [HttpPost]
        public async Task<ActionResult<GrupoNomina>> Crear(CrearGrupoNominaRequest comando)
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

        #region PUT
        // PUT: ApiV3.GrupoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GrupoNominas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarGrupoNominaRequest comando)
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
        #region PATCH
        // PATCH: ApiV3.GrupoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GrupoNominas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialGrupoNominaRequest comando)
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

        #region DELETE
        // DELETE: ApiV3.ApiV3.ithActions/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GrupoNominas_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administradora>> Eliminar(int id)
        {
            var comando = new EliminarGrupoNominaRequest
            {
                Id = id
            };
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
