using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU0102_AdministrarGastosViaje

namespace ApiV3.Controllers.Odata
{

    public class GastoViajesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public GastoViajesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/GastoViajes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GastoViajes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<GastoViaje>> Get()
        {
            return this.contexto.GastoViajes;
        }

        // GET: odata/GastoViajes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GastoViajes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<GastoViaje> Get([FromODataUri] int key)
        {
            IQueryable<GastoViaje> result = this.contexto.GastoViajes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
