using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU040_CausalTerminacion

namespace ApiV3.Controllers
{

    public class CausalTerminacionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CausalTerminacionesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/CausalTerminaciones

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CausalTerminaciones_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<CausalTerminacion>> Get()
        {
            return this.contexto.CausalTerminaciones;
        }

        // GET: odata/CausalTerminaciones/5

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CausalTerminaciones_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public SingleResult<CausalTerminacion> Get([FromODataUri] int key)
        {
            IQueryable<CausalTerminacion> result = this.contexto.CausalTerminaciones.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
