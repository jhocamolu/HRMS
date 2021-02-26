using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU047_Tipo_Ausentismo
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{

    public class TipoAusentismosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoAusentismosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/TipoAusentismos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAusentismos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoAusentismo>> Get()
        {
            return this.contexto.TipoAusentismos;
        }

        // GET: api/TipoAusentismos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAusentismos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoAusentismo> Get([FromODataUri] int key)
        {
            IQueryable<TipoAusentismo> result = this.contexto.TipoAusentismos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
