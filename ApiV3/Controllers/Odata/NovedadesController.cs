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
    public class NovedadesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NovedadesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/Novedades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Novedades_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<Novedad>> Get()
        {
            return contexto.Novedades;
        }

        // GET: api/Novedades/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Novedades_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Novedad> Get([FromODataUri] int key)
        {
            IQueryable<Novedad> result = this.contexto.Novedades.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
