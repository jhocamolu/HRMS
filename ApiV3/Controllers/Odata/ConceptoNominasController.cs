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
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{

    public class ConceptoNominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public ConceptoNominasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/ConceptoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<ConceptoNomina>> Get()
        {
            return this.contexto.ConceptoNominas;
        }

        // GET: odata/ConceptoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.ConceptoNominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<ConceptoNomina> Get([FromODataUri] int key)
        {
            IQueryable<ConceptoNomina> result = this.contexto.ConceptoNominas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
