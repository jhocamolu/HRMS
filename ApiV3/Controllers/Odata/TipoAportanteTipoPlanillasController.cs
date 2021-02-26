using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class TipoAportanteTipoPlanillasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoAportanteTipoPlanillasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoAportanteTipoPlanillas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAportanteTipoPlanillas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoAportanteTipoPlanilla>> Get()
        {
            return this.contexto.TipoAportanteTipoPlanillas;
        }

        // GET: odata/TipoAportanteTipoPlanillas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAportanteTipoPlanillas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoAportanteTipoPlanilla> Get([FromODataUri] int key)
        {
            IQueryable<TipoAportanteTipoPlanilla> result = this.contexto.TipoAportanteTipoPlanillas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
