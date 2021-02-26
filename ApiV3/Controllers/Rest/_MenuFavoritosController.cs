using ApiV3.Dominio._MenuFavoritos.Comandos.Crear;
using ApiV3.Dominio._MenuFavoritos.Comandos.Eliminar;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class _MenuFavoritosController : ControllerBase
    {
        private readonly IMediator mediador;

        public _MenuFavoritosController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        // POST: api/_MenuFavoritos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos._MenuFavoritos_Crear })]
        [HttpPost]
        public async Task<ActionResult<_MenuFavorito>> Post_MenuFavorito([FromBody] CrearMenuFavoritoRequest comando)
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

        // DELETE: api/_MenuFavoritos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos._MenuFavoritos_Eliminar })]
        [HttpDelete("{id}")]
        public async Task<ActionResult<_MenuFavorito>> Delete_MenuFavorito(int id, [FromBody] EliminarMenuFavoritoRequest comando)
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

    }
}
