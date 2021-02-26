using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU013_Tipo_Moneda
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{

    public class ActividadEconomicasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ActividadEconomicasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/ActividadEconomicas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadEconomicas_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<ActividadEconomica>> Get()
        {
            return this.contexto.ActividadEconomicas;
        }

        // GET: api/ActividadEconomicas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadEconomicas_Obtener })]
        [HttpGet]
        [EnableQuery]

        public SingleResult<ActividadEconomica> Get([FromODataUri] int key)
        {
            IQueryable<ActividadEconomica> result = this.contexto.ActividadEconomicas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
