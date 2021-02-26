using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU014_Administrar_Calendarios
namespace ApiV3.Controllers.Odata
{
    public class CalendariosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CalendariosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/Calendarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Calendarios_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Calendario>> Get()
        {
            return contexto.Calendarios;
        }

        // GET: odata/Calendarios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Calendarios_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<Calendario> Get([FromODataUri] int key)
        {
            IQueryable<Calendario> result = this.contexto.Calendarios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
