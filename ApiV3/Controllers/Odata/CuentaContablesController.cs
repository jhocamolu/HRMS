using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class CuentaContablesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public CuentaContablesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/CuentaContables
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaContables_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<CuentaContable> Get()
        {
            return this.contexto.CuentaContables;
        }

        //GET: odata/CuentaContables/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CuentaContables_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<CuentaContable> Get([FromODataUri] int key)
        {
            IQueryable<CuentaContable> cuentaContables = this.contexto.CuentaContables.Where(p => p.Id == key);
            return SingleResult.Create(cuentaContables);
        }
    }
}
