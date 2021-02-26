using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU059_Administrar_Libranza
/// Controlador Odata para busqueda personalizada
namespace ApiV3.Controllers
{
    public class LibranzasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public LibranzasController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: odata/Libranzas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Libranzas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<Libranza> Get()
        {
            return this.contexto.Libranzas;
        }

        // GET: odata/Libranzas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Libranzas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<Libranza> Get([FromODataUri] int key)
        {
            IQueryable<Libranza> result = this.contexto.Libranzas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
