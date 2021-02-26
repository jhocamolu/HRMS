using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  Parametro Generales
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class ParametroGeneralesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ParametroGeneralesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/ParametroGenerales
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ParametroGenerales_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ParametroGeneral>> Get()
        {
            return this.contexto.ParametroGenerales;
        }

        // GET: api/ParametroGenerales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ParametroGenerales_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<ParametroGeneral> Get([FromODataUri] int key)
        {
            IQueryable<ParametroGeneral> result = this.contexto.ParametroGenerales.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
