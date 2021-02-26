using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class TipoCotizanteSubtipoCotizantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoCotizanteSubtipoCotizantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoCotizanteSubtipoCotizantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCotizanteSubtipoCotizantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoCotizanteSubtipoCotizante>> Get()
        {
            return this.contexto.TipoCotizanteSubtipoCotizantes;
        }

        // GET: odata/TipoCotizanteSubtipoCotizantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCotizanteSubtipoCotizantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoCotizanteSubtipoCotizante> Get([FromODataUri] int key)
        {
            IQueryable<TipoCotizanteSubtipoCotizante> result = this.contexto.TipoCotizanteSubtipoCotizantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
