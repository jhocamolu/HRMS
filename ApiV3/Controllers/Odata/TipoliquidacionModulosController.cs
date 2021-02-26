using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Alberio Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// @Description HU050_Tipos de liquidaciones
namespace ApiV3.Controllers.Odata
{
    public class TipoLiquidacionModulosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TipoLiquidacionModulosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/TipoLiquidacionModulos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionModulo_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<TipoLiquidacionModulo> Get()
        {
            return this.contexto.TipoLiquidacionModulos;
        }

        //GET: odata/TipoLiquidacionModulos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoLiquidacionModulo_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<TipoLiquidacionModulo> Get([FromODataUri] int key)
        {
            IQueryable<TipoLiquidacionModulo> tipoLiquidacionModulo = this.contexto.TipoLiquidacionModulos.Where(p => p.Id == key);
            return SingleResult.Create(tipoLiquidacionModulo);
        }
    }
}