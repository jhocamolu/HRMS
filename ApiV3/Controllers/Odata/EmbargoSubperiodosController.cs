using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class EmbargoSubperiodosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public EmbargoSubperiodosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/EmbargoSubperiodos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Embargos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<EmbargoSubperiodo>> Get()
        {
            return this.contexto.EmbargoSubperiodos;
        }

        //GET: odata/EmbargoSubperiodos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.EmbargoSubperiodos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<EmbargoSubperiodo> Get([FromODataUri] int key)
        {
            IQueryable<EmbargoSubperiodo> embargoSubperiodos = this.contexto.EmbargoSubperiodos.Where(p => p.Id == key);
            return SingleResult.Create(embargoSubperiodos);
        }
    }
}
