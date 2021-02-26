using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU017_Informacion_Familiar
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionFamiliaresController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public InformacionFamiliaresController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: odata/InformacionFamiliares
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.InformacionFamiliares_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<InformacionFamiliar>> Get()
        {
            return this.contexto.InformacionFamiliares;
        }

        // GET: odata/InformacionFamiliares/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.InformacionFamiliares_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<InformacionFamiliar> Get([FromODataUri] int key)
        {
            IQueryable<InformacionFamiliar> result = this.contexto.InformacionFamiliares.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
