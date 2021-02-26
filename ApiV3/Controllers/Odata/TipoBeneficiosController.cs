using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description HU058 Tipos de Beneficios
namespace ApiV3.Controllers.Odata
{
    public class TipoBeneficiosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoBeneficiosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoBeneficios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoBeneficios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<TipoBeneficio>> Get()
        {
            return contexto.TipoBeneficios;
        }

        // GET: api/TipoBeneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoBeneficios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoBeneficio> Get([FromODataUri] int key)
        {
            IQueryable<TipoBeneficio> result = this.contexto.TipoBeneficios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
