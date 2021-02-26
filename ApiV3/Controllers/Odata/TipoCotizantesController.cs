using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class TipoCotizantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoCotizantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoCotizantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCotizantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoCotizante>> Get()
        {
            return this.contexto.TipoCotizantes;
        }

        // GET: odata/TipoCotizantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoCotizantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoCotizante> Get([FromODataUri] int key)
        {
            IQueryable<TipoCotizante> result = this.contexto.TipoCotizantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
