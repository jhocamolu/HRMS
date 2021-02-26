using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU016_Tipo_Moneda
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class TipoMonedasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoMonedasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoMonedas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoMonedas_Obtener })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoMoneda>> Get()
        {
            return this.contexto.TipoMonedas;
        }

        // GET: api/TipoMonedas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoMonedas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<TipoMoneda> Get([FromODataUri] int key)
        {
            IQueryable<TipoMoneda> result = this.contexto.TipoMonedas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
