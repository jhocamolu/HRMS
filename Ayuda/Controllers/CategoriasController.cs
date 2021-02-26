using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ayuda.Models;
using Microsoft.AspNet.OData;
using Ayuda.Dominio.Categorias.Comandos.Crear;
using Ayuda.Dominio.Categorias.Comandos.Actualizar;
using Ayuda.Dominio.Categorias.Comandos.Eliminar;
using Ayuda.Dominio.Categorias.Consultas.ObtenerCategoria;
using Ayuda.Dominio.Categorias.Consultas.ObtenerCategorias;
using Ayuda.Dominio.Categorias.Comandos.Parcial;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Ayuda.Controllers
{
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly AyudaDbContext context;

        public CategoriasController(IMediator mediator, AyudaDbContext context)
        {
            this.mediator = mediator;
            this.context = context;
        }


        #region ODATA
        // GET: odata/Categorias
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            var query = new ObtenerCategoriasRequest();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // GET: odata/Categorias/5
        [HttpGet]
        [EnableQuery]
        public SingleResult<Categoria> Get([FromODataUri] int key)
        {
            IQueryable<Categoria> resultado = this.context.Categorias.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
        #endregion

        #region PUT
        // PUT: api/Categorias/5
        [HttpPut("api/[controller]/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody]ActualizarCategoriaRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            TryValidateModel(command);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await mediator.Send(command);
            if (result.IsSuccess) return Ok(result.Data);
            return StatusCode(500, new
            {
                Message = result.FailureReason
            });
        }
        #endregion
        #region PATCH
        // PATCH: api/Categorias
        [HttpPatch("api/[controller]/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialCategoriaRequest comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            TryValidateModel(comando);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await mediator.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion
        #region POST
        // POST: api/Categorias
        [HttpPost("api/[controller]")]
        public async Task<ActionResult<Categoria>> Crear([FromBody]CrearCategoriaRequest command)
        {
            TryValidateModel(command);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return StatusCode(500, new
            {
                Message = result.FailureReason
            });
        }
        #endregion
        #region DELETE
        // DELETE: api/Categorias/5
        [HttpDelete("api/[controller]/{id}")]
        public async Task<ActionResult<Categoria>> Eliminar(int id)
        {
            var command = new EliminarCategoriaRequest
            {
                Id = id
            };
            TryValidateModel(command);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await mediator.Send(command);
            if (result.IsSuccess) return Ok(result.Data);
            return StatusCode(500, new
            {
                Message = result.FailureReason
            });
        }
        #endregion
    }
}
