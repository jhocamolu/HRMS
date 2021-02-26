using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU059-Libranzas
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibranzaSubperiodosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public LibranzaSubperiodosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/LibranzaSubperiodos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibranzaSubperiodos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<LibranzaSubperiodo> Get()
        {
            return this.contexto.LibranzaSubperiodos;
        }

        // GET: api/LibranzaSubperiodos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LibranzaSubperiodos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<LibranzaSubperiodo> Get([FromODataUri] int key)
        {
            IQueryable<LibranzaSubperiodo> resultado = this.contexto.LibranzaSubperiodos.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
