using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU007_Administrar_Idiomas

namespace ApiV3.Controllers.Odata
{
    public class IdiomasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public IdiomasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/Idiomas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Idiomas_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Idioma>> Get()
        {
            return contexto.Idiomas;
        }

        // GET: odata/Idiomas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Idiomas_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<Idioma> Get([FromODataUri] int key)
        {
            IQueryable<Idioma> result = this.contexto.Idiomas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
