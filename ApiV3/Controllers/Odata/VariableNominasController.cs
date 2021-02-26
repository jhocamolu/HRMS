using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class VariableNominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public VariableNominasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/VariableNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.VariableNominas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<VariableNomina>> Get()
        {
            return this.contexto.VariableNominas;
        }

        // GET: odata/VariableNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.VariableNominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<VariableNomina> Get([FromODataUri] int key)
        {
            IQueryable<VariableNomina> result = this.contexto.VariableNominas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
