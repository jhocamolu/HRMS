using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU024
namespace ApiV3.Controllers.Odata
{
    public class DependenciaJerarquiasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public DependenciaJerarquiasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/DependenciaJerarquias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DependenciaJerarquias_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<DependenciaJerarquia>> Get()
        {
            return this.contexto.DependenciaJerarquias;
        }

        // GET: odata/DependenciaJerarquias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DependenciaJerarquias_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<DependenciaJerarquia> Get([FromODataUri] int key)
        {
            IQueryable<DependenciaJerarquia> result = this.contexto.DependenciaJerarquias.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
