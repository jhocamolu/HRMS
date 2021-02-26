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

    public class AplicacionExternaCargoDependientesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public AplicacionExternaCargoDependientesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/AplicacionExternaCargoDependientes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternaCargoDependientes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<AplicacionExternaCargoDependiente> Get()
        {
            return this.contexto.AplicacionExternaCargoDependientes;
        }

        // GET: odata/AplicacionExternaCargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternaCargoDependientes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<AplicacionExternaCargoDependiente> Get([FromODataUri] int key)
        {
            IQueryable<AplicacionExternaCargoDependiente> result = this.contexto.AplicacionExternaCargoDependientes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
