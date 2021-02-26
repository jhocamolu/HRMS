using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Plantillas.Models;
using System.Linq;

/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com

namespace Plantillas.Controllers.Odata
{

    public class ComplementoPlantillasController : ControllerBase
    {
        private readonly PlantillasDbContext contexto;

        public ComplementoPlantillasController(PlantillasDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/ComplementoPlantillas
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<ComplementoPlantilla>> Get()
        {
            return contexto.ComplementoPlantillas;
        }

        // GET: odata/ComplementoPlantillas/5
        [HttpGet]
        [EnableQuery]
        public SingleResult<ComplementoPlantilla> Get([FromODataUri] int key)
        {
            IQueryable<ComplementoPlantilla> result = this.contexto.ComplementoPlantillas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
