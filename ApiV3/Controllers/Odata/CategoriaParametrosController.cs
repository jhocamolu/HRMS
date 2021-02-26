using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  Categoria parametro
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{

    public class CategoriaParametrosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CategoriaParametrosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/CategoriaParametros
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaParametros_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<CategoriaParametro>> Get()
        {
            return this.contexto.CategoriaParametros;
        }

        // GET: api/CategoriaParametros/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaParametros_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<CategoriaParametro> Get([FromODataUri] int key)
        {
            IQueryable<CategoriaParametro> result = this.contexto.CategoriaParametros.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
