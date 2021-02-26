using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU025_CargoReporta

namespace ApiV3.Controllers.Odata
{

    public class CargoReportasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CargoReportasController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: odata/CargoReportas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoReportas_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<CargoReporta>> Get()
        {
            return this.contexto.CargoReportas;
        }

        // GET: odata/CargoReportas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoReportas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public SingleResult<CargoReporta> Get([FromODataUri] int key)
        {
            IQueryable<CargoReporta> result = this.contexto.CargoReportas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
