using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Models;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Odata
{
    public class GrupoDocumentoEtiquetasController : ControllerBase
    {
        private readonly PlantillasDbContext _context;

        public GrupoDocumentoEtiquetasController(PlantillasDbContext context)
        {
            _context = context;
        }

        // GET: api/GrupoDocumentoEtiquetas
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<GrupoDocumentoEtiqueta>> Get()
        {
            return _context.GrupoDocumentoEtiquetas;
        }

        [HttpGet]
        [EnableQuery]
        public SingleResult<GrupoDocumentoEtiqueta> Get([FromODataUri] int key)
        {
            IQueryable<GrupoDocumentoEtiqueta> result = this._context.GrupoDocumentoEtiquetas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }


    }
}
