using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU029_Experiencia_Laboral
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class ExperienciaLaboralesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ExperienciaLaboralesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/ExperienciaLaborales
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ExperienciaLaborales_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<ExperienciaLaboral>> Get()
        {
            return this.contexto.ExperienciaLaborales;
        }

        // GET: odata/ExperienciaLaborales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ExperienciaLaborales_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<ExperienciaLaboral> Get([FromODataUri] int key)
        {
            IQueryable<ExperienciaLaboral> result = this.contexto.ExperienciaLaborales.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
