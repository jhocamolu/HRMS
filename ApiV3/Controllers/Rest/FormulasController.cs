using ApiV3.Dominio.Formulas.Comandos.EjecutarFormulas;
using ApiV3.Dominio.Formulas.Comandos.ParcialFormula;
using ApiV3.Dominio.Formulas.Comandos.VerificarFomula;
using ApiV3.Infraestructura.DbContexto;
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
    public class FormulasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        private readonly IMediator mediador;

        public FormulasController(NominaDbContext contexto, IMediator mediador)
        {
            this.contexto = contexto;
            this.mediador = mediador;
        }


        // GET: api/Formulas/verificar/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Formulas_Verificar })]
        [HttpPost("{id}")]
        public async Task<ActionResult> Verificar(int id, [FromBody] VerificarFormulaRequest comando)
        {
            if (id != comando.Id)
            {
                return BadRequest();
            }
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok();
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

        // PATCH: ApiV3.Formulas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Formulas_CambiarEstadoRegistro })]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Parcial(int id, [FromBody] ParcialFormulaRequest comando)
        {
            Console.WriteLine(id);
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

        // GET: api/Formulas/verificar/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Formulas_Ejecutar })]
        [HttpPost("Ejecutar")]
        public async Task<ActionResult> Ejecutar()
        {
            var comando = new EjecutarFormulaRequest();
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok();
            return StatusCode(500, new
            {
                Message = resultado.FailureReason
            });
        }

    }
}

