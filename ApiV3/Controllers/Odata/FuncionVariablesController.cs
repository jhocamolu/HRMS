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

    public class FuncionVariablesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public FuncionVariablesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/FuncionVariables
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionVariables_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<FuncionVariable>> Get()
        {
            return this.contexto.FuncionVariables;
        }


        // GET: odata/FuncionVariables/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionVariables_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<FuncionVariable> Get([FromODataUri] int key)
        {
            IQueryable<FuncionVariable> result = this.contexto.FuncionVariables.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
