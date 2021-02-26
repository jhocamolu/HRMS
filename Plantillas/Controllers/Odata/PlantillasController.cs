using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Models;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Odata
{
    public class PlantillasController : ControllerBase
    {
        private readonly PlantillasDbContext contexto;

        public PlantillasController(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/Plantillas
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Plantilla>> Get()
        {
            return contexto.Plantillas;
        }

        // GET: odata/Plantillas/5
        [HttpGet]
        [EnableQuery]
        public SingleResult<Plantilla> Get([FromODataUri] int key)
        {
            IQueryable<Plantilla> result = this.contexto.Plantillas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
