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
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadSubperiodosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NovedadSubperiodosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/NovedadSubperiodos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NovedadSubperiodos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<NovedadSubperiodo>> Get()
        {
            return contexto.NovedadSubperiodos;
        }

        // GET: api/NovedadSubperiodos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NovedadSubperiodos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<NovedadSubperiodo> Get([FromODataUri] int key)
        {
            IQueryable<NovedadSubperiodo> result = this.contexto.NovedadSubperiodos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
