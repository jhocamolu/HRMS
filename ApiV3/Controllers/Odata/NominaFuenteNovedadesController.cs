using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU055_NovedadesNomina
/// Controlador Odata para busqueda personalizada
namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominaFuenteNovedadesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NominaFuenteNovedadesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/NominaFuenteNovedades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuenteNovedades_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<NominaFuenteNovedad>> Get()
        {
            return this.contexto.NominaFuenteNovedades;
        }

        // GET: api/NominaFuenteNovedades/5
        //

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuenteNovedades_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<NominaFuenteNovedad> Get([FromODataUri] int key)
        {
            IQueryable<NominaFuenteNovedad> result = this.contexto.NominaFuenteNovedades.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
