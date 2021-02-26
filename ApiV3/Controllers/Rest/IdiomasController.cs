using ApiV3.Dominio.Idiomas.Comandos.Actualizar;
using ApiV3.Dominio.Idiomas.Comandos.Crear;
using ApiV3.Dominio.Idiomas.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU007_Administrar_Idiomas

namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdiomasController : ControllerBase
    {
        private readonly IMediator mediador;

        public IdiomasController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region PUT
        // PUT: ApiV3.Idiomas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Idiomas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarIdiomaRequest comando)
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
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Idiomas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialIdiomaRequest comando)
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

        #region POST
        // POST: ApiV3.Idiomas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Idiomas_Crear })]
        [HttpPost]
        public async Task<ActionResult<Idioma>> Crear([FromBody] CrearIdiomaRequest comando)
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

        //#region DELETE
        //// DELETE: ApiV3.Idiomas/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Idioma>> Eliminar(int id, EliminarIdiomaRequest comando)
        //{
        //   if (id != comando.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var resultado = await mediador.Send(comando);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode(500, new
        //    {
        //        Message = resultado.FailureReason
        //    });
        //}
        //#endregion

    }
}
