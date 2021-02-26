using ApiV3.Dominio.EjecucionTareas.Comandos.ActualizarEstadoFuncionarioContrato;
using ApiV3.Dominio.EjecucionTareas.Comandos.NotificacionContrato;
using ApiV3.Dominio.EjecucionTareas.Comandos.NotificarRequisicion;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjecucionTareasController : ControllerBase
    {
        private readonly IMediator mediador;

        public EjecucionTareasController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // GET: api/EjecucionTareas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EjecucionTareas_ObtenerNotificacionContrato })]
        [HttpPatch("NotificacionVencimientoContrato")]
        public async Task<ActionResult> TareaProgramadaFinalizar(NotificacionContratoRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }


        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EjecucionTareas_TareaProgramadaActualizarEstadoFuncionarioContrato })]
        //PATCH: Api/EjecucionTareas/ActualizarEstadoFuncionarioContrato
        [HttpPatch("ActualizarEstadoFuncionarioContrato")]
        public async Task<ActionResult> TareaProgramadaFinalizar([FromBody] ActualizarEstadoFuncionarioContratoRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
        #endregion

        //[TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EjecucionTareas_ObtenerNotificacionRequisicion })]
        [HttpPatch("NotificarVencimientoCubrirVacante")]
        public async Task<ActionResult> NotificarVencimientoCubrirVacante(NotificarRequisicionRequest parcialTarea)
        {
            var resultado = await mediador.Send(parcialTarea);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { message = resultado.FailureReason });
        }
    }
}
