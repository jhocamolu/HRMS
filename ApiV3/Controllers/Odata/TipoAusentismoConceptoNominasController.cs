using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU047_AdministrarTipoAusentismoConceptoNomina

namespace ApiV3.Controllers.Odata
{
    public class TipoAusentismoConceptoNominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoAusentismoConceptoNominasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoAusentismoConceptoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAusentismoConceptoNominas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<TipoAusentismoConceptoNomina>> Get()
        {
            return contexto.TipoAusentismoConceptoNominas;
        }

        // GET: api/TipoAusentismoConceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAusentismoConceptoNominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoAusentismoConceptoNomina> Get([FromODataUri] int key)
        {
            IQueryable<TipoAusentismoConceptoNomina> result = this.contexto.TipoAusentismoConceptoNominas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
