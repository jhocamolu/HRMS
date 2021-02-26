using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ayuda.Models;
using MediatR;
using Ayuda.Dominio.Articulos.Comandos.Crear;
using Microsoft.AspNet.OData;
using Ayuda.Dominio.Articulos.Consultas.ObtenerArticulos;
using Ayuda.Dominio.Articulos.Consultas.ObtenerArticulo;
using Ayuda.Dominio.Articulos.Comandos.Eliminar;
using Ayuda.Dominio.Articulos.Comandos.Actualizar;
using Ayuda.Dominio.Articulos.Consultas.ObtenerPalabra;
using Ayuda.Dominio.Articulos.Comandos.Parcial;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// 

namespace Ayuda.Controllers
{

    public class ArticulosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ArticulosController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        #region odata
        // GET: api/Articulos
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Articulo>>> Get()
        {
            var query = new ObtenerArticulosRequest();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        // GET: api/Articulos/5
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<Articulo>> Get([FromODataUri] int key)
        {
            var consulta = new ObtenerArticuloRequest() { Id = key };
            var categoria = await mediator.Send(consulta);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        #endregion
        #region rest

        [HttpGet("api/[controller]/{palabra}")]
        public async Task<ActionResult<IEnumerable<Articulo>>> Palabra(string palabra)
        {
            if (palabra == null)
            {
                return BadRequest();
            }
            var query = new ObtenerPalabraRequest
            {
                Palabra = palabra
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }



        // PUT: api/Articulos/5
        [HttpPut("api/[controller]/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody]ActualizarArticuloRequest command)
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

        #region PATCH
        // PATCH: api/Articulos
        [HttpPatch("api/[controller]/{id}")]
        public async Task<IActionResult> Estado(int id, [FromBody] ParcialArticuloRequest comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            var resultado = await mediator.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        #endregion

        // POST: api/Articulos
        [HttpPost("api/[controller]")]
        public async Task<ActionResult<Articulo>> Crear([FromBody]CrearArticuloRequest command)
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

        // DELETE: api/Articulos/5
        [HttpDelete("api/[controller]/{id}")]
        public async Task<ActionResult<Articulo>> Eliminar(int id)
        {
            var command = new EliminarArticuloRequest
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
