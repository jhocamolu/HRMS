using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU009_Gestionar_Administradoras
namespace ApiV3.Controllers.Odata
{
    public class AdministradorasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public AdministradorasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/Administradoras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Administradoras_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Administradora>> Get()
        {
            return contexto.Administradoras;
        }

        // GET: api/Administradoras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Administradoras_Obtener })]
        [HttpGet()]
        [EnableQuery]
        public SingleResult<Administradora> Get([FromODataUri] int key)
        {
            IQueryable<Administradora> result = this.contexto.Administradoras.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
