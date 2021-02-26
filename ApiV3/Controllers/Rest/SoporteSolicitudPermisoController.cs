using ApiV3.Dominio.SoporteSolicitudPermisos.Comandos.Actualizar;
using ApiV3.Dominio.SoporteSolicitudPermisos.Comandos.Crear;
using ApiV3.Dominio.SoporteSolicitudPermisos.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU066_SolicitudPermisos
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoporteSolicitudPermisosController : ControllerBase
    {
        private readonly IMediator mediador;

        public SoporteSolicitudPermisosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region POST              
        // POST: api/SoporteSolicitudPermiso
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SoporteSolicitudPermiso_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearSoporteSolicitudPermisoRequest comando)
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
        // PUT: api/SoporteSolicitudPermiso/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SoporteSolicitudPermiso_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarSoporteSolicitudPermisoRequest comando)
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
        // DELETE: api/Eliminar/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SoporteSolicitudPermiso_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarSoporteSolicitudPermisoRequest
            {
                Id = id
            };
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

    }
}
