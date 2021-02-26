using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Models;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Odata
{
    public class EtiquetasController : ControllerBase
    {
        private readonly PlantillasDbContext contexto;

        public EtiquetasController(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/Etiquetas
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Etiqueta>> Get()
        {
            return this.contexto.Etiquetas;
        }

        // GET: odata/Etiquetas/5
        [HttpGet]
        [EnableQuery]
        public SingleResult<Etiqueta> Get([FromODataUri] int key)
        {
            IQueryable<Etiqueta> result = this.contexto.Etiquetas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
