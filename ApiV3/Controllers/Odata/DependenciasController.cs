using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU024
namespace ApiV3.Controllers.Odata
{
    public class DependenciasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public DependenciasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/Dependencias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Dependencias_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Dependencia>> Get()
        {
            return this.contexto.Dependencias;
        }

        // GET: odata/Dependencias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Dependencias_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<Dependencia> Get([FromODataUri] int key)
        {
            IQueryable<Dependencia> result = this.contexto.Dependencias.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
