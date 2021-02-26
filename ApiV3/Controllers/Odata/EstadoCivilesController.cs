using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU003_Administrar_Estados_Civiles
namespace ApiV3.Controllers.Odata
{
    public class EstadoCivilesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public EstadoCivilesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/EstadoCiviles
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EstadoCiviles_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<EstadoCivil>> Get()
        {
            return contexto.EstadoCiviles;
        }

        // GET: api/EstadoCiviles/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EstadoCiviles_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<EstadoCivil> Get([FromODataUri] int key)
        {
            IQueryable<EstadoCivil> result = this.contexto.EstadoCiviles.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
