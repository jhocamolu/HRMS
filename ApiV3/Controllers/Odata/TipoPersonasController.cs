using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class TipoPersonasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoPersonasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoPersonas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoPersonas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoPersona>> Get()
        {
            return this.contexto.TipoPersonas;
        }

        // GET: odata/TipoPersonas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoPersonas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoPersona> Get([FromODataUri] int key)
        {
            IQueryable<TipoPersona> result = this.contexto.TipoPersonas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
