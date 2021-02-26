using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU065_SolicitudCesantias

namespace ApiV3.Controllers.Odata
{

    public class SolicitudCesantiasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SolicitudCesantiasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/SolicitudCesantias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudCesantias_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<SolicitudCesantia> Get()
        {
            return this.contexto.SolicitudCesantias;
        }

        // GET: odata/SolicitudCesantias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudCesantias_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<SolicitudCesantia> Get([FromODataUri] int key)
        {
            IQueryable<SolicitudCesantia> consulta = this.contexto.SolicitudCesantias.Where(p => p.Id == key);
            return SingleResult.Create(consulta);
        }
    }
}
