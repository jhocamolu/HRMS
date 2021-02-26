using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU025-AdministrarCargo CargoGrados
/// Controlador Odata para busqueda personalizada
namespace ApiV3.Controllers.Odata
{

    public class CargoGradosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CargoGradosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/CargoGrados
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoGrados_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<CargoGrado>> Get()
        {
            return contexto.CargoGrados;
        }

        // GET: api/CargoGrados/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoGrados_Obtener })]
        [HttpGet]
        [EnableQuery]

        public SingleResult<CargoGrado> Get([FromODataUri] int key)
        {
            IQueryable<CargoGrado> result = this.contexto.CargoGrados.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
