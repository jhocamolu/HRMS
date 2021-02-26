using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class ClaseAportantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ClaseAportantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/ClaseAportantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroTrabajos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<ClaseAportante>> Get()
        {
            return this.contexto.ClaseAportantes;
        }

        // GET: odata/ClaseAportantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseAportantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<ClaseAportante> Get([FromODataUri] int key)
        {
            IQueryable<ClaseAportante> result = this.contexto.ClaseAportantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
