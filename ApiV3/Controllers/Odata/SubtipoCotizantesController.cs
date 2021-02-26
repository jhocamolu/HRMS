using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class SubtipoCotizantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SubtipoCotizantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/SubtipoCotizantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SubtipoCotizantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<SubtipoCotizante>> Get()
        {
            return this.contexto.SubtipoCotizantes;
        }

        // GET: odata/SubtipoCotizantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SubtipoCotizantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<SubtipoCotizante> Get([FromODataUri] int key)
        {
            IQueryable<SubtipoCotizante> result = this.contexto.SubtipoCotizantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
