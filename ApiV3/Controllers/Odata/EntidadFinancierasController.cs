using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU012_Administrar_Entidades_Financieras
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class EntidadFinancierasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public EntidadFinancierasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/EntidadFinancieras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EntidadFinancieras_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<EntidadFinanciera>> Get()
        {
            return contexto.EntidadFinancieras;
        }

        // GET: api/EntidadFinancieras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EntidadFinancieras_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public SingleResult<EntidadFinanciera> Get([FromODataUri] int key)
        {
            IQueryable<EntidadFinanciera> result = this.contexto.EntidadFinancieras.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}