using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{
    public class JuzgadosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public JuzgadosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Juzgados
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Juzgados_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public IQueryable<Juzgado> Get()
        {
            return this.contexto.Juzgados;
        }

        // GET: odata/Juzgados/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Juzgados_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<Juzgado> Get([FromODataUri] int key)
        {
            IQueryable<Juzgado> juzgados = this.contexto.Juzgados.Where(p => p.Id == key);
            return SingleResult.Create(juzgados);
        }
    }
}
