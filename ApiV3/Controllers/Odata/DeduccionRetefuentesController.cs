using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class DeduccionRetefuentesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public DeduccionRetefuentesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/DeduccionRetefuente
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DeduccionRetefuentes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<DeduccionRetefuente>> Get() => this.contexto.DeduccionRetefuentes;

        //GET: odata/DeduccionRetefuente/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DeduccionRetefuentes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<DeduccionRetefuente> Get([FromODataUri] int key)
        {
            IQueryable<DeduccionRetefuente> otroSis = this.contexto.DeduccionRetefuentes.Where(p => p.Id == key);
            return SingleResult.Create(otroSis);
        }
    }
}
