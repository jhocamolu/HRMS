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
    public class LicenciaConduccionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public LicenciaConduccionesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/LicenciaConducciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LicenciaConducciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<LicenciaConduccion>> Get()
        {
            return contexto.LicenciaConducciones;
        }

        // GET: api/LicenciaConducciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LicenciaConducciones_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public async Task<ActionResult<LicenciaConduccion>> Get([FromODataUri] int key)
        {
            var licenciaConduccion = await contexto.LicenciaConducciones.FindAsync(key);

            if (licenciaConduccion == null)
            {
                return NotFound();
            }

            return licenciaConduccion;
        }
    }
}
