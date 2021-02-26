using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class TipoAportantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoAportantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoAportantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAportantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoAportante>> Get()
        {
            return this.contexto.TipoAportantes;
        }

        // GET: odata/TipoAportantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAportantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoAportante> Get([FromODataUri] int key)
        {
            IQueryable<TipoAportante> result = this.contexto.TipoAportantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
