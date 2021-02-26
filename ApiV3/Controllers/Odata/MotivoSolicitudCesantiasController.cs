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

    public class MotivoSolicitudCesantiasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public MotivoSolicitudCesantiasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/MotivoSolicitudCesantias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.MotivoSolicitudCesantias_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<MotivoSolicitudCesantia> Get()
        {
            return this.contexto.MotivoSolicitudCesantias;
        }

        // GET: api/MotivoSolicitudCesantias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.MotivoSolicitudCesantias_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<MotivoSolicitudCesantia> Get([FromODataUri] int key)
        {
            IQueryable<MotivoSolicitudCesantia> consulta = this.contexto.MotivoSolicitudCesantias.Where(p => p.Id == key);
            return SingleResult.Create(consulta);
        }
    }
}
