using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU010_Administrar_Centro_Trabajos
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{
    public class ParentescosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public ParentescosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/Parentescos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Parentescos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Parentesco>> Get()
        {
            return this.contexto.Parentescos;
        }

        // GET: api/Parentescos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Parentescos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<Parentesco> Get([FromODataUri] int key)
        {
            IQueryable<Parentesco> result = this.contexto.Parentescos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
