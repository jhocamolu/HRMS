using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU052_registrar_informacion_básica_liquidación_nomina
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoContablesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public PeriodoContablesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/PeriodoContables
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.PeriodoContables_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public ActionResult<IQueryable<PeriodoContable>> Get()
        {
            return this.contexto.PeriodoContables;
        }

        // GET: api/PeriodoContables/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.PeriodoContables_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<PeriodoContable> Get([FromODataUri] int key)
        {
            IQueryable<PeriodoContable> result = this.contexto.PeriodoContables.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
