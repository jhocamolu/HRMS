using ApiV3.Dominio.Beneficios.Comandos.Actualizar;
using ApiV3.Dominio.Beneficios.Comandos.Crear;
using ApiV3.Dominio.Beneficios.Comandos.Eliminar;
using ApiV3.Dominio.Beneficios.Comandos.Estado;
using ApiV3.Dominio.Beneficios.Comandos.Parcial;
using ApiV3.Dominio.Beneficios.Consultas;
using ApiV3.Dominio.Beneficios.TareasProgramadas;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// Controlador
/// Sprint8

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiosController : ControllerBase
    {
        private readonly IMediator mediador;

        public BeneficiosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        #region PUT
        //PUT: api/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Beneficios_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarBeneficioRequest beneficio)
        {
            if (id != beneficio.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region POST

        // POST: api/Beneficios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Beneficios_Crear })]
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] CrearBeneficioRequest beneficio)
        {
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region PATCH
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Beneficios_CambiarEstadoRegistro })]
        // PATCH: api/[Beneficios/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialBeneficioRequest beneficio)
        {
            if (id != beneficio.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Beneficios_CambiarEstado })]
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] EstadoBeneficioRequest beneficio)
        {
            if (id != beneficio.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region DELETE
        // DELETE: api/Beneficios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var beneficio = new EliminarBeneficioRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(beneficio);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        #region GET
        //Busca los valores actiuales del contrato(contrato/OtroSi)
        [HttpGet("{Id}/RequisitoBeneficioAdjunto")]
        public async Task<ActionResult<object>> Obtener(int Id)
        {
            var otroSi = new ObtenerBeneficioRequest()
            {
                Id = Id,
            };
            var resultado = await mediador.Send(otroSi);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region 
        // Patch: api/LibroVacaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Beneficios_TareaProgramadaProcesar })]
        [HttpPost("TareaProgramadaProcesar")]
        public async Task<ActionResult> TareaProgramadaActualizar()
        {
            var comando = new ProcesarBeneficioRequest();
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
