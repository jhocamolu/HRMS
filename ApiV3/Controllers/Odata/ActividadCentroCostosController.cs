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
/// 
namespace ApiV3.Controllers.Odata
{

    public class ActividadCentroCostosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ActividadCentroCostosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/ActividadCentroCostos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadCentroCostos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<ActividadCentroCosto> Get()
        {
            return this.contexto.ActividadCentroCostos;
        }

        // GET: odata/ActividadCentroCostos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ActividadCentroCostos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<ActividadCentroCosto> Get([FromODataUri] int key)
        {
            IQueryable<ActividadCentroCosto> result = this.contexto.ActividadCentroCostos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
