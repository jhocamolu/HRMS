using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU006_Administrar_Ocupaciones
namespace ApiV3.Controllers.Odata
{
    public class OcupacionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public OcupacionesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/Ocupaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Ocupaciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<Ocupacion>> Get()
        {
            return contexto.Ocupaciones;
        }

        // GET: api/Ocupaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Ocupaciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Ocupacion> Get([FromODataUri] int key)
        {
            IQueryable<Ocupacion> result = this.contexto.Ocupaciones.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
