using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{

    public class TipoPlanillasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoPlanillasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoPlanillas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoPlanillas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoPlanilla>> Get()
        {
            return this.contexto.TipoPlanillas;
        }

        // GET: odat/TipoPlanillas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoPlanillas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoPlanilla> Get([FromODataUri] int key)
        {
            IQueryable<TipoPlanilla> result = this.contexto.TipoPlanillas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
