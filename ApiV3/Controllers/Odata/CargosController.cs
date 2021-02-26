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

    public class CargosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CargosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Cargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Cargos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Cargo>> Get()
        {
            return this.contexto.Cargos;
        }

        // GET: odata/Cargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Cargos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public SingleResult<Cargo> Get([FromODataUri] int key)
        {
            IQueryable<Cargo> result = this.contexto.Cargos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
