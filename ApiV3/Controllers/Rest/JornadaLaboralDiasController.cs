using ApiV3.Dominio.JornadaLaboralDias.Comandos.Actualizar;
using ApiV3.Dominio.JornadaLaboralDias.Comandos.Crear;
using ApiV3.Dominio.JornadaLaboralDias.Comandos.Eliminar;
using ApiV3.Dominio.JornadaLaboralDias.Comandos.Parcial;
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
    public class JornadaLaboralDiasController : ControllerBase
    {
        public readonly IMediator mediador;
        public JornadaLaboralDiasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: ApiV3.JornadaLaboralDias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaboralDias_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarJornadaLaboralDiaRequest jornadaLaboralDia)
        {
            if (id != jornadaLaboralDia.Id) return BadRequest();
            var resultado = await mediador.Send(jornadaLaboralDia);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST
        // POST: ApiV3.JornadaLaboralDias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaboralDias_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearJornadaLaboralDiaRequest jornadaLaboral)
        {
            var resultado = await mediador.Send(jornadaLaboral);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region DELETE
        //delete: ApiV3.JornadaLaboralDias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaboralDias_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var jornadaLaboralDia = new EliminarJornadaLaboralDiaRequest { Id = id };
            var resultado = await mediador.Send(jornadaLaboralDia);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new { message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        //PATCH: ApiV3.JornadaLaboralDias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaboralDias_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialJornadaLaboralDiaRequest jornadaLaboralDia)
        {
            if (id != jornadaLaboralDia.Id) return BadRequest();

            var resultado = await mediador.Send(jornadaLaboralDia);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
    }
}
