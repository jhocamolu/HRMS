using ApiV3.Dominio.NominaFuncionarios.Comandos.Crear;
using ApiV3.Dominio.NominaFuncionarios.Comandos.Eliminar;
using ApiV3.Dominio.NominaFuncionarios.Comandos.Finalizar;
using ApiV3.Dominio.NominaFuncionarios.Comandos.Iniciar;
using ApiV3.Dominio.NominaFuncionarios.Comandos.Listar;
using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominaFuncionariosController : ControllerBase
    {
        private readonly NominaDbContext _context;
        private readonly IMediator mediador;

        public NominaFuncionariosController(NominaDbContext context, IMediator mediador)
        {
            _context = context;
            this.mediador = mediador;
        }

        // POST: api/NominaFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_CrearListado })]
        [HttpPost("listar")]
        public async Task<ActionResult<NominaFuncionario>> Listar(int id, [FromBody] ListarNominaFuncionarioRequest command)
        {
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        // POST: api/NominaFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_Crear })]
        [HttpPost]
        public async Task<ActionResult<NominaFuncionario>> Crear([FromBody] CrearNominaFuncionarioRequest command)
        {
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        // PATCH: api/NominaFuncionarios/iniciar
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_Iniciar })]
        [HttpPatch("iniciar")]
        public async Task<ActionResult<NominaFuncionario>> Iniciar([FromBody] IniciarNominaFuncionarioRequest command)
        {
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        // PATCH: api/NominaFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_Finalizar })]
        [HttpPatch("finalizar")]
        public async Task<ActionResult<NominaFuncionario>> Finalizar([FromBody] FinalizarNominaFuncionarioRequest command)
        {
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess)
            {
                return Ok(resultado.Data);
            }
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        // DELETE: api/NominaFuncionarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_EliminarUno })]
        [HttpDelete("eliminarUno/{id}")]
        public async Task<ActionResult<NominaFuncionario>> EliminarUno(int id)
        {
            var command = new EliminarNominaFuncionarioRequest
            {
                Id = id
            };
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_EliminarFuncionarios })]
        [HttpDelete("eliminarFuncionarios/{id}")]
        public async Task<ActionResult<NominaFuncionario>> EliminarFuncionarios(int id, [FromBody] JObject funcionarios)
        {
            List<int> fun = new List<int>();
            foreach (var item in funcionarios["funcionarios"].Values().ToList())
            {
                fun.Add((int)item);
            }
            var command = new EliminarNominaFuncionarioRequest
            {
                NominaId = id,
                Funcionarios = fun
            };
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_LimpiarNomina })]
        [HttpDelete("limpiarNomina/{id}")]
        public async Task<ActionResult<NominaFuncionario>> LimpiarNomina(int id)
        {
            var command = new EliminarNominaFuncionarioRequest
            {
                NominaId = id
            };
            var resultado = await mediador.Send(command);
            if (resultado.IsSuccess) return Ok(resultado.Data);
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }
    }
}
