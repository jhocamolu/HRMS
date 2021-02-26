using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU025_Administrar_Cargos
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class CargoGruposController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CargoGruposController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/CargoGrupos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoGrupos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<CargoGrupo> Get()
        {
            return this.contexto.CargoGrupos;
        }

        // GET: odata/CargoGrupos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoGrupos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<CargoGrupo> Get([FromODataUri] int key)
        {
            IQueryable<CargoGrupo> result = this.contexto.CargoGrupos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
