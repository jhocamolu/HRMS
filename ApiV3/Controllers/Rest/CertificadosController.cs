
using ApiV3.Dominio.Certificados.Consultas.Contrato;
using ApiV3.Dominio.Certificados.Consultas.Sueldo;
using ApiV3.Dominio.Certificados.Consultas.SueldoContrato;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadosController : ControllerBase
    {
        private readonly IMediator mediador;

        public CertificadosController(IMediator mediador)
        {
            this.mediador = mediador;
        }


        #region GET Sueldo
        // GET: ApiV3.Archivos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Certificados_ObtenerSueldo })]
        [HttpGet("{id}/sueldo")]
        public async Task<ActionResult> ObtenerSueldo(string Id)
        {
            var comando = new ObtenerSueldoRequest()
            {
                Id = Id,
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region GET Contrato
        // GET: ApiV3.Archivos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Certificados_ObtenerCargo })]
        [HttpGet("{id}/Cargo")]
        public async Task<ActionResult> ObtenerCargo(string Id)
        {
            var comando = new ObtenerCargoRequest()
            {
                Id = Id,
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion

        #region GET SueldoContrato
        // GET: ApiV3.Archivos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Certificados_ObtenerSueldoCargo })]
        [HttpGet("{id}/SueldoCargo")]
        public async Task<ActionResult> ObtenerSueldoContrato(string Id)
        {
            var comando = new ObtenerSueldoCargoRequest()
            {
                Id = Id,
            };
            var resultado = await mediador.Send(comando);
            if (resultado.IsSuccess) return Ok(resultado.Data);

            return StatusCode(500, new { Message = resultado.FailureReason });
        }
        #endregion
    }
}
