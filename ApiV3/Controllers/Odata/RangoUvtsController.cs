using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU079_AdmininstrarRangoUvt


namespace ApiV3.Controllers.Odata
{
    public class RangoUvtsController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public RangoUvtsController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/RangoUvts
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RangoUvts_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<RangoUvt> Get()
        {
            return this.contexto.RangoUvts;
        }

        // GET: odata/RangoUvts/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RangoUvts_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<RangoUvt> Get([FromODataUri] int key)
        {
            IQueryable<RangoUvt> result = this.contexto.RangoUvts.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
