using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU023_Administrar_Nivel_Cargos
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class NivelCargosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NivelCargosController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: odata/NivelCargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelCargos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<NivelCargo>> Get()
        {
            return this.contexto.NivelCargos;
        }

        // GET: odata/NivelCargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NivelCargos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<NivelCargo> Get([FromODataUri] int key)
        {
            IQueryable<NivelCargo> nivelCargos = this.contexto.NivelCargos.Where(x => x.Id == key);

            return SingleResult.Create(nivelCargos);
        }
    }
}
