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
    public class TipoLiquidacionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoLiquidacionesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoLiquidaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoLiquidacion>> Get()
        {
            return contexto.TipoLiquidaciones;
        }

        // GET: api/TipoLiquidaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidaciones_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<TipoLiquidacion> Get([FromODataUri] int key)
        {
            IQueryable<TipoLiquidacion> result = this.contexto.TipoLiquidaciones.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
