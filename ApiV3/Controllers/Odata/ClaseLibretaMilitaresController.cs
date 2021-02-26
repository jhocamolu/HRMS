using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Odata
{

    public class ClaseLibretaMilitaresController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ClaseLibretaMilitaresController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/ClaseLibretaMilitares
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseLibretaMilitares_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<ClaseLibretaMilitar>> Get()
        {
            return contexto.ClaseLibretaMilitares;
        }

        // GET: api/ClaseLibretaMilitares/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseLibretaMilitares_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public async Task<ActionResult<ClaseLibretaMilitar>> Get([FromODataUri] int key)
        {
            var claseLibretaMilitar = await contexto.ClaseLibretaMilitares.FindAsync(key);

            if (claseLibretaMilitar == null)
            {
                return NotFound();
            }

            return claseLibretaMilitar;
        }
    }
}
