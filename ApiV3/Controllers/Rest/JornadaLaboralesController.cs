using ApiV3.Dominio.JornadaLaborales.Comandos.Actualizar;
using ApiV3.Dominio.JornadaLaborales.Comandos.Crear;
using ApiV3.Dominio.JornadaLaborales.Comandos.Eliminar;
using ApiV3.Dominio.JornadaLaborales.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU034_Administrar_Jornadas_Laborales
/// Controlador REST
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class JornadaLaboralesController : ControllerBase
    {
        private readonly IMediator mediador;
        public JornadaLaboralesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.JornadaLaborales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaborales_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarJornadaLaboralRequest jornadaLaboral)
        {
            if (id != jornadaLaboral.Id) return BadRequest();
            var resultado = await mediador.Send(jornadaLaboral);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.JornadaLaborales
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaborales_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearJornadaLaboralRequest jornadaLaboral)
        {
            var resultado = await mediador.Send(jornadaLaboral);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.JornadaLaborales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaborales_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var jornadaLaboral = new EliminarJornadaLaboralRequest { Id = id };
            var resultado = await mediador.Send(jornadaLaboral);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.JornadaLaborales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaborales_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialJornadaLaboralRequest jornadaLaboral)
        {
            if (id != jornadaLaboral.Id) return BadRequest();

            var resultado = await mediador.Send(jornadaLaboral);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

    }
}
