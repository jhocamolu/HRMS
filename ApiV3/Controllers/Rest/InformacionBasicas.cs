using ApiV3.Dominio.InformacionBasicas.Comandos.Actualizar;
using ApiV3.Dominio.InformacionBasicas.Comandos.Parcial;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Rest
{
    /// @author Jesus Albeiro Gaviria R
    /// @email  desarrollador5@alcanosesp.com
    /// @Description HU019Administrar_informacion_basica_compania
    /// Controlador

    [Route("api/[controller]")]
    [ApiController]
    public class InformacionBasicas : ControllerBase
    {


        public readonly IMediator mediador;
        public InformacionBasicas(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region PUT
        //PUT: ApiV3.informacionBasica/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.InformacionBasicas_Actualizar })]
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] ActualizarInformacionBasicaRequest informacion)
        {
            if (id != informacion.Id) return BadRequest();
            var resultado = await mediador.Send(informacion);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Mesaage = resultado.FailureReason });
        }
        #endregion PUT

        #region POST    
        //[HttpPost]
        //public async Task<ActionResult> Crear([FromBody]CrearInformacionBasicaRequest informacion)
        //{
        //    var resultado = await mediador.Send(informacion);
        //    if (resultado.IsSuccess) return Ok(resultado.Data);

        //    return StatusCode(500, new { message = resultado.FailureReason });
        //}
        #endregion POST

        #region DELETE
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Eliminar(int id)
        //{
        //    var informacionbasica = new EliminarInformacionBasicaRequest { Id = id };
        //    var resultado = await this.mediador.Send(informacionbasica);

        //    if (resultado.IsSuccess) return Ok(resultado.Data);
        //    return StatusCode( (int)resultado.Code, new { Message = resultado.FailureReason });
        //}
        #endregion DELETE

        #region PATCH
        //PATCH api/[controller]ipoDocumentos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.InformacionBasicas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Parcial(int id, [FromBody] ParcialInformacionBasicaRequest informacionBasica)
        {
            if (id != informacionBasica.Id) return BadRequest();

            var resultado = await mediador.Send(informacionBasica);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion
    }
}
