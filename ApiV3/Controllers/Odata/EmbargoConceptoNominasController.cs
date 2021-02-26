using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU060_Embargos
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbargoConceptoNominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public EmbargoConceptoNominasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/EmbargoConceptoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DocumentoFuncionarios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<EmbargoConceptoNomina> Get()
        {
            return this.contexto.EmbargoConceptoNominas;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EmbargoConceptoNominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<EmbargoConceptoNomina> Get([FromODataUri] int key)
        {
            IQueryable<EmbargoConceptoNomina> consulta = this.contexto.EmbargoConceptoNominas.Where(p => p.Id == key);
            return SingleResult.Create(consulta);
        }
    }
}
