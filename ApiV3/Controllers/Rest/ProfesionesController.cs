using ApiV3.Dominio.Profesiones.Comandos.Actualizar;
using ApiV3.Dominio.Profesiones.Comandos.Crear;
using ApiV3.Dominio.Profesiones.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU005_Administrar_Profesiones
/// Controlador 

namespace ApiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionesController : ControllerBase
    {
        private readonly IMediator mediador;
        public ProfesionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        // PUT: ApiV3.Profesiones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Profesiones_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarProfesionRequest profesion)
        {
            if (id != profesion.Id) return BadRequest();

            var resultado = await mediador.Send(profesion);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.Profesiones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Profesiones_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearProfesionRequest profesion)
        {
            var resultado = await mediador.Send(profesion);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE 
        // DELETE: ApiV3.Profesiones/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var profesion = new EliminarProfesionRequest { Id = id };

        //    var resultado = await mediador.Send(profesion);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode(500, new { Message = resultado.FailureReason });
        //}

        #endregion

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Profesiones_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialProfesionRequest profesiones)
        {
            if (id != profesiones.Id)
            {
                return BadRequest();
            }

            var resultado = await mediador.Send(profesiones);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
