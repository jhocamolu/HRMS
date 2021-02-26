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
    public class ConceptoNominaCuentaContablesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ConceptoNominaCuentaContablesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/ConceptoNominaCuentaContables
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaCuentaContables_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ConceptoNominaCuentaContable>> Get()
        {
            var a = this.contexto.ConceptoNominaCuentaContables;

            return a;
        }

        // GET: odata/ConceptoNominaCuentaContables/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominaCuentaContables_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ConceptoNominaCuentaContable> Get([FromODataUri] int key)
        {
            IQueryable<ConceptoNominaCuentaContable> result = this.contexto.ConceptoNominaCuentaContables.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
