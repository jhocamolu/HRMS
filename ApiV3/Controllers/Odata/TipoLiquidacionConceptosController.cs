using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU050_Tipos de liquidaciones

namespace ApiV3.Controllers.Odata
{
    public class TipoLiquidacionConceptosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoLiquidacionConceptosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/LiquidacionConceptos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionConceptos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoLiquidacionConcepto>> Get()
        {
            return contexto.TipoLiquidacionConceptos;
        }

        // GET: api/LiquidacionConceptos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionConceptos_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<TipoLiquidacionConcepto> Get([FromODataUri] int key)
        {
            IQueryable<TipoLiquidacionConcepto> result = this.contexto.TipoLiquidacionConceptos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
