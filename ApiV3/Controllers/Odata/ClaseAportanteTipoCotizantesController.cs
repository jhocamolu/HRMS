using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class ClaseAportanteTipoCotizantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ClaseAportanteTipoCotizantesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odat/ClaseAportanteTipoCotizantes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseAportanteTipoCotizantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<ClaseAportanteTipoCotizante>> Get()
        {
            return this.contexto.ClaseAportanteTipoCotizantes;
        }

        // GET: odata/ClaseAportanteTipoCotizantes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseAportanteTipoCotizantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<ClaseAportanteTipoCotizante> Get([FromODataUri] int key)
        {
            IQueryable<ClaseAportanteTipoCotizante> result = this.contexto.ClaseAportanteTipoCotizantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
