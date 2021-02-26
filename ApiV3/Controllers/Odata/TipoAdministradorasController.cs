using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{

    public class TipoAdministradorasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoAdministradorasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        // GET: api/TipoAdministradoras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAdministradoras_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoAdministradora>> Get()
        {
            return contexto.TipoAdministradoras;
        }

        // GET: api/TipoAdministradoras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAdministradoras_Obtener })]
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<TipoAdministradora>> GetGet([FromODataUri] int key)
        {
            var tipoAdministradora = await contexto.TipoAdministradoras.FindAsync(key);

            if (tipoAdministradora == null)
            {
                return NotFound();
            }

            return tipoAdministradora;
        }
    }
}
