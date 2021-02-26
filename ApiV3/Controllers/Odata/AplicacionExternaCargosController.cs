using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU069_AplicacionExterna

namespace ApiV3.Controllers.Odata
{

    public class AplicacionExternaCargosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public AplicacionExternaCargosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/AplicacionExternaCargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternaCargos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<AplicacionExternaCargo> Get()
        {
            return this.contexto.AplicacionExternaCargos;
        }

        // GET: api/AplicacionExternaCargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternaCargos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<AplicacionExternaCargo> Get([FromODataUri] int key)
        {
            IQueryable<AplicacionExternaCargo> result = this.contexto.AplicacionExternaCargos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
