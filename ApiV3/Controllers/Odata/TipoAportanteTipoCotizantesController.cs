using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class TipoAportanteTipoCotizantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoAportanteTipoCotizantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoAportanteTipoCotizantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAportanteTipoCotizantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoAportanteTipoCotizante>> Get()
        {
            return this.contexto.TipoAportanteTipoCotizantes;
        }

        // GET: odata/TipoAportanteTipoCotizantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAportanteTipoCotizantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoAportanteTipoCotizante> Get([FromODataUri] int key)
        {
            IQueryable<TipoAportanteTipoCotizante> result = this.contexto.TipoAportanteTipoCotizantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
