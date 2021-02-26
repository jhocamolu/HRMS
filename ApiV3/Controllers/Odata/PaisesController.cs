using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{

    public class PaisesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public PaisesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Paises_Listar })]
        // GET: odata/Paises
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Pais>> Get()
        {
            return contexto.Paises;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Paises_Obtener })]
        // GET: api/Paises/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]

        public SingleResult<Pais> Get([FromODataUri] int key)
        {
            IQueryable<Pais> result = this.contexto.Paises.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }


    }
}
