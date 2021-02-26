using ApiV3.Dominio.SolicitudVacaciones.Comandos.Actualizar;
using ApiV3.Dominio.SolicitudVacaciones.Comandos.Crear;
using ApiV3.Dominio.SolicitudVacaciones.Comandos.Estado;
using ApiV3.Dominio.SolicitudVacaciones.Comandos.Parcial;
using ApiV3.Dominio.SolicitudVacaciones.TareasProgramadas.Actualizar;
using ApiV3.Dominio.SolicitudVacaciones.TareasProgramadas.ActualizarInterrupcion;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU064_SolicitudVacaciones
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudVacacionesController : ControllerBase
    {
        private readonly IMediator mediador;

        public SolicitudVacacionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region GET
        // Get: api/LibroVacaciones
        /*[HttpGet]
        public async Task<ActionResult> TareaProgramadaActualizar()
        {
            var comando = new ActualizarSolicitudVacacionesRequest();
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }*/
        #endregion
        #region POST
        // POST: api/solicitudVacaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearSolicitudVacacionRequest comando)
        {
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion  
        #region PUT
        // PUT: api/solicitudVacaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ActualizarSolicitudVacacionRequest comando)
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
        // PATCH: api/solicitudVacaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialSolicitudVacacionRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_CambiarEstado })]
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoSolicitudVacacionRequest comando)
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
        //PATCH: Api/solicitudVacaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_TareaProgramadaActualizar })]
        [HttpPatch("TareaProgramadaActualizar")]
        public async Task<ActionResult> TareaProgramadaActualizar([FromBody] ActualizarSolicitudVacacionesRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion
        #region PATCH
        //PATCH: Api/solicitudVacaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacaciones_TareaProgramadaActualizarInterrupcion })]
        [HttpPatch("TareaProgramadaActualizarInterrupcion")]
        public async Task<ActionResult> TareaProgramadaActualizarInterrupcion([FromBody] ActualizarSolicitudVacacionesInterrupcionRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

    }
}
