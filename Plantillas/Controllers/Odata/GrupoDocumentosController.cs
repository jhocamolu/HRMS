using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Models;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Odata
{
    public class GrupoDocumentosController : ControllerBase
    {
        private readonly PlantillasDbContext _context;

        public GrupoDocumentosController(PlantillasDbContext context)
        {
            _context = context;
        }

        // GET: odata/GrupoDocumentos
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<GrupoDocumento>> Get()
        {
            return _context.GrupoDocumentos;
        }

        // GET: odata/GrupoDocumentos/5
        [HttpGet]
        [EnableQuery]
        public SingleResult<GrupoDocumento> Get([FromODataUri] int key)
        {
            IQueryable<GrupoDocumento> result = this._context.GrupoDocumentos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
