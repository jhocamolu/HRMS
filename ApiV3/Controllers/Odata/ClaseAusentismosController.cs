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

    public class ClaseAusentismosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ClaseAusentismosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/ClaseAusentismos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseAusentismos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<ClaseAusentismo>> Get()
        {
            return this.contexto.ClaseAusentismos;
        }

        // GET: odata/ClaseAusentismos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseAusentismos_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<ClaseAusentismo> Get([FromODataUri] int key)
        {
            IQueryable<ClaseAusentismo> result = this.contexto.ClaseAusentismos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
