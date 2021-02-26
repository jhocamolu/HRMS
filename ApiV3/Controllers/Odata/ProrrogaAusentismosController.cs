using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU048_Prorroga_Ausentismo
/// 
namespace ApiV3.Controllers.Odata
{

    public class ProrrogaAusentismosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ProrrogaAusentismosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/ProrrogaAusentismos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ProrrogaAusentismos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<ProrrogaAusentismo>> Get()
        {
            return this.contexto.ProrrogaAusentismos;
        }

        // GET: odata/ProrrogaAusentismos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ProrrogaAusentismos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public SingleResult<ProrrogaAusentismo> Get([FromODataUri] int key)
        {
            IQueryable<ProrrogaAusentismo> prorrogaAusentismo = this.contexto.ProrrogaAusentismos.Where(x => x.Id == key);
            return SingleResult.Create(prorrogaAusentismo);
        }
    }
}
