using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class HoraExtrasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public HoraExtrasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/HoraExtras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HoraExtras_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<HoraExtra>> Get()
        {
            return this.contexto.HoraExtras;
        }

        // GET: odata/HoraExtras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.HoraExtras_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<HoraExtra> Get([FromODataUri] int key)
        {
            IQueryable<HoraExtra> query = this.contexto.HoraExtras.Where(p => p.Id == key);
            return SingleResult.Create(query);
        }

    }
}
