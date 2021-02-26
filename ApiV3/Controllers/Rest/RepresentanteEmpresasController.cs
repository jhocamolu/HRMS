using ApiV3.Dominio.RepresentanteEmpresas.Comandos.Actualizar;
using ApiV3.Dominio.RepresentanteEmpresas.Comandos.Crear;
using ApiV3.Dominio.RepresentanteEmpresas.Comandos.Eliminar;
using ApiV3.Dominio.RepresentanteEmpresas.Comandos.Parcial;
using ApiV3.Dominio.RepresentanteEmpresas.Consultas.Obtener;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentanteEmpresasController : ControllerBase
    {
        private readonly IMediator mediador;

        public RepresentanteEmpresasController(IMediator mediador)
        {
            this.mediador = mediador;
        }
        #region DELETE
        // DELETE: ApiV3.ApiV3.ithActions/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RepresentanteEmpresas_ObtenerSlug })]
        [HttpGet("gruposlug/{slug}/fecha/{fecha}")]
        public async Task<ActionResult> obtener(string slug, DateTime fecha)
        {
            var comando = new ObtenerRepresentanteEmpresaRequest
            {
                GrupoDocumentoSlug = slug,
                FechaInicio = fecha,
                FechaFin = fecha
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
        #region POST
        // POST: api/[controller]epresentanteEmpresas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RepresentanteEmpresas_Crear })]
        public async Task<ActionResult> Crear([FromBody] CrearRepresentanteEmpresaRequest comando)
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
        //PUT: api/[controller]epresentanteEmpresas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RepresentanteEmpresas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActuailzarRepresentanteEmpresaRequest comando)
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
        // PATCH: api/[controller]epresentanteEmpresas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RepresentanteEmpresas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialRepresentanteEmpresaRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RepresentanteEmpresas_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var comando = new EliminarRepresentanteEmpresaRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode((int)resultado.Code, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
    }
}
