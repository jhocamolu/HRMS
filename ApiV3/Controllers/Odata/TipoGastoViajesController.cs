using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU0100_AdministrarTipoGastosViaje
/// 
namespace ApiV3.Controllers.Odata
{

    public class TipoGastoViajesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoGastoViajesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoGastoViajes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoGastoViajes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoGastoViaje>> Get()
        {
            return this.contexto.TipoGastoViajes;
        }

        // GET: odata/TipoGastoViajes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoGastoViajes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<TipoGastoViaje> Get([FromODataUri] int key)
        {
            IQueryable<TipoGastoViaje> result = this.contexto.TipoGastoViajes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
