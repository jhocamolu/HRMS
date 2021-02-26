using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class TipoSoportesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TipoSoportesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/TipoSoportes
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoSoportes_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoSoporte>> Get()
        {
            return this.contexto.TipoSoportes;
        }

        //GET: odata/TipoSoportes/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoSoportes_Obtener })]
        [HttpGet]
        [EnableQuery]

        public SingleResult<TipoSoporte> Get([FromODataUri] int key)
        {
            IQueryable<TipoSoporte> result = this.contexto.TipoSoportes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
