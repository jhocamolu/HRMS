using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Models;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Odata
{
    public class DocumentosController : ControllerBase
    {
        private readonly PlantillasDbContext _context;

        public DocumentosController(PlantillasDbContext context)
        {
            _context = context;
        }

        // GET: api/Documentos
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Documento>> Get()
        {
            return _context.Documentos;
        }

        // GET: api/Documentos/5
        [HttpGet]
        [EnableQuery]
        public SingleResult<Documento> Get([FromODataUri] int key)
        {
            IQueryable<Documento> result = this._context.Documentos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
