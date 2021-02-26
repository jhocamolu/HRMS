using ApiV3.Dominio.LibroVacaciones.TareasProgramadas.Actualizar;
using ApiV3.Dominio.LibroVacaciones.TareasProgramadas.Migrar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU06_LibroVacaciones
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroVacacionesController : ControllerBase
    {
        private readonly IMediator mediador;

        public LibroVacacionesController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PATCH Actualizar
        // Patch: api/LibroVacaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibroVacaciones_TareaProgramadaActualizar })]
        [HttpPatch("TareaProgramadaActualizar")]
        public async Task<ActionResult> TareaProgramadaActualizar()
        {
            var comando = new ActualizarLibroVacacionesRequest();
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion
        
        #region PATCH Migracion
        // Patch: api/LibroVacaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibroVacaciones_TareaProgramadaActualizar })]
        [HttpPatch("TareaProgramadaMigrar")]
        public async Task<ActionResult> TareaProgramadaMigrar()
        {
            var comando = new MigrarLibroVacacionesRequest();
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode((int)resultado.Code, new { Message = resultado.FailureReason });
        }
        #endregion
    }
}