using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU066_SolicitudPermisos
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoporteSolicitudPermisosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SoporteSolicitudPermisosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/SoporteSolicitudPermisos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SoporteSolicitudPermiso_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<SoporteSolicitudPermiso> Get()
        {
            return this.contexto.SoporteSolicitudPermisos;
        }

        // GET: odata/SoporteSolicitudPermisos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SoporteSolicitudPermiso_Obtener })]
        [HttpGet]

        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<SoporteSolicitudPermiso> Get([FromODataUri] int key)
        {
            IQueryable<SoporteSolicitudPermiso> consulta = this.contexto.SoporteSolicitudPermisos.Where(p => p.Id == key);
            return SingleResult.Create(consulta);
        }
    }
}
