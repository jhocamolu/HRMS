using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU019_Informacion_basica
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class TipoContribuyentesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoContribuyentesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/TipoContribuyentes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoContribuyentes_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoContribuyente>> Get()
        {
            return contexto.TipoContribuyentes;
        }

        // GET: api/TipoContribuyentes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoContribuyentes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<TipoContribuyente> Get([FromODataUri] int key)
        {
            IQueryable<TipoContribuyente> result = this.contexto.TipoContribuyentes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
