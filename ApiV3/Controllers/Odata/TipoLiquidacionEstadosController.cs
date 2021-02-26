using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description HU050_Tipos de liquidaciones
namespace ApiV3.Controllers.Odata
{

    public class TipoLiquidacionEstadosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoLiquidacionEstadosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoLiquidacionEstados
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionEstados_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public IQueryable<TipoLiquidacionEstado> Get()
        {
            return this.contexto.TipoLiquidacionEstados;
        }

        // GET: api/TipoLiquidacionEstados/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionEstados_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<TipoLiquidacionEstado> Get([FromODataUri] int key)
        {
            IQueryable<TipoLiquidacionEstado> result = this.contexto.TipoLiquidacionEstados.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
