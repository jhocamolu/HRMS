using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{
    public class ConceptoBasesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ConceptoBasesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoBases_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ConceptoBase>> Get()
        {
            return this.contexto.ConceptoBases;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoBases_Obtener })]
        // GET: odata/ConceptoBases/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<ConceptoBase> Get([FromODataUri] int key)
        {
            var query = this.contexto.ConceptoBases.Where(p => p.ConceptoNominaId == key);
            IQueryable<ConceptoBase> result = query;
            return result;
        }

    }
}
