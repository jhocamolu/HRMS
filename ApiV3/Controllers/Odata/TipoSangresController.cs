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
    public class TipoSangresController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoSangresController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoSangres
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoSangres_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoSangre>> Get()
        {
            return contexto.TipoSangres;
        }

        // GET: api/TipoSangres/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoSangres_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public async Task<ActionResult<TipoSangre>> Get([FromODataUri] int key)
        {
            var tipoSangre = await contexto.TipoSangres.FindAsync(key);

            if (tipoSangre == null)
            {
                return NotFound();
            }

            return tipoSangre;
        }

    }
}
