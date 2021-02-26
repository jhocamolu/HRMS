using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU046_Concepto_Nomina
/// Controlador Odata para ClaseAportanteTipoAportantesController requerido pila

namespace ApiV3.Controllers.Odata
{
    public class ClaseAportanteTipoAportantesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ClaseAportanteTipoAportantesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoCotizanteTipoPlanillas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseAportanteTipoAportantes_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ClaseAportanteTipoAportante>> Get()
        {
            return contexto.ClaseAportanteTipoAportantes;
        }

        // GET: api/TipoCotizanteTipoPlanillas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ClaseAportanteTipoAportantes_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ClaseAportanteTipoAportante> Get([FromODataUri] int key)
        {
            IQueryable<ClaseAportanteTipoAportante> result = this.contexto.ClaseAportanteTipoAportantes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}