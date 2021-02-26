using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class HojaDeVidaExperienciaLaboralesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public HojaDeVidaExperienciaLaboralesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/HojaDeVidaExperienciaLaborale
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaExperienciaLaborales_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<HojaDeVidaExperienciaLaboral>> Get()
        {
            return this.contexto.HojaDeVidaExperienciaLaborales;
        }

        // GET: odata/HojaDeVidaExperienciaLaborale/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HojaDeVidaExperienciaLaborales_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<HojaDeVidaExperienciaLaboral> Get([FromODataUri] int key)
        {
            IQueryable<HojaDeVidaExperienciaLaboral> result = this.contexto.HojaDeVidaExperienciaLaborales.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
