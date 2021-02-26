using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class TipoBeneficioRequisitosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoBeneficioRequisitosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoBeneficioRequisitos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoBeneficioRequisitos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<TipoBeneficioRequisito>> Get()
        {
            return contexto.TipoBeneficioRequisitos;
        }

        // GET: api/TipoBeneficioRequisitos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoBeneficioRequisitos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoBeneficioRequisito> Get(int key)
        {
            IQueryable<TipoBeneficioRequisito> result = this.contexto.TipoBeneficioRequisitos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
