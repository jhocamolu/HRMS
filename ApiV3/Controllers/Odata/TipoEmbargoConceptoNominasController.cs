using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU051_Tipo_Embargo

namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEmbargoConceptoNominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoEmbargoConceptoNominasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/TipoEmbargoConceptoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargoConceptoNominas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<TipoEmbargoConceptoNomina> Get()
        {
            return this.contexto.TipoEmbargoConceptoNominas;
        }

        // GET: odata/TipoEmbargoConceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargoConceptoNominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoEmbargoConceptoNomina> Get([FromODataUri] int key)
        {
            IQueryable<TipoEmbargoConceptoNomina> result = this.contexto.TipoEmbargoConceptoNominas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
