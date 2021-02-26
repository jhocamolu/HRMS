using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU010_Administrar_Centro_Trabajos
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class CentroTrabajosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CentroTrabajosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/CentroTrabajos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroTrabajos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<CentroTrabajo>> Get()
        {
            return this.contexto.CentroTrabajos;
        }

        // GET: api/CentroTrabajos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroTrabajos_Obtener })]
        [HttpGet]
        [EnableQuery]

        public SingleResult<CentroTrabajo> Get([FromODataUri] int key)
        {
            IQueryable<CentroTrabajo> result = this.contexto.CentroTrabajos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
