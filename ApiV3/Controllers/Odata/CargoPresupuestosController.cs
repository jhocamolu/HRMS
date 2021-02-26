using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoPresupuestosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CargoPresupuestosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/CargoPresupuestos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoPresupuestos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<CargoPresupuesto> Get()
        {
            return this.contexto.CargoPresupuestos;
        }

        // GET: api/CargoPresupuestos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoPresupuestos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<CargoPresupuesto> Get([FromODataUri] int key)
        {
            IQueryable<CargoPresupuesto> result = this.contexto.CargoPresupuestos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
