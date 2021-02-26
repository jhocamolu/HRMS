using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU070_AdmininstrarDistribucionCosto

namespace ApiV3.Controllers.Odata
{

    public class ActividadesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ActividadesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Actividades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Actividades_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Actividad> Get()
        {
            return this.contexto.Actividades;
        }

        // GET: odata/Actividades/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Actividades_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Actividad> Get([FromODataUri] int key)
        {
            IQueryable<Actividad> result = this.contexto.Actividades.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
