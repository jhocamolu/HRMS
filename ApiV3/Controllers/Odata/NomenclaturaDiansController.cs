using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description Administrar Nomenclatura Dian
namespace ApiV3.Controllers.Odata
{

    public class NomenclaturaDiansController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NomenclaturaDiansController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/NomenclaturaDians
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NomenclaturaDians_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<NomenclaturaDian>> Get()
        {
            return contexto.NomenclaturaDians;
        }

        // GET: api/NomenclaturaDians/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NomenclaturaDians_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<NomenclaturaDian> Get([FromODataUri] int key)
        {
            IQueryable<NomenclaturaDian> result = this.contexto.NomenclaturaDians.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
