using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class EmbargosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public EmbargosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Embargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Embargos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<Embargo>> Get()
        {
            return this.contexto.Embargos;
        }

        //GET: odata/Embargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Embargos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<Embargo> Get([FromODataUri] int key)
        {
            IQueryable<Embargo> embargo = this.contexto.Embargos.Where(p => p.Id == key);
            return SingleResult.Create(embargo);
        }
    }
}
