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
    public class SolicitudPermisosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SolicitudPermisosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/SolicitudPermisos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudPermisos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<SolicitudPermiso> Get()
        {
            return this.contexto.SolicitudPermisos;
        }


        // GET: odata/SolicitudPermisos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudPermisos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<SolicitudPermiso> Get([FromODataUri] int key)
        {
            IQueryable<SolicitudPermiso> consulta = this.contexto.SolicitudPermisos.Where(p => p.Id == key);
            return SingleResult.Create(consulta);
        }
    }
}
