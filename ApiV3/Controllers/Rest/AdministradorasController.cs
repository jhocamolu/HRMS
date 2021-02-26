using ApiV3.Dominio.Administradoras.Comandos.Actualizar;
using ApiV3.Dominio.Administradoras.Comandos.Crear;
using ApiV3.Dominio.Administradoras.Comandos.Eliminar;
using ApiV3.Dominio.Administradoras.Comandos.Estado;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU009_Gestionar_Administradoras

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorasController : ControllerBase
    {
        private readonly IMediator mediador;

        public AdministradorasController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        // PUT: ApiV3.Administradoras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Administradoras_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarAdministradoraRequest comando)
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

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Administradoras_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialAdministradoraRequest comando)
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
        // POST: ApiV3.Administradoras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Administradoras_Crear })]
        [HttpPost]
        public async Task<ActionResult<Administradora>> Crear(CrearAdministradoraRequest comando)
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

        // DELETE: ApiV3.Administradoras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Administradoras_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administradora>> Eliminar(int id)
        {
            var comando = new EliminarAdministradoraRequest
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

    }
}
