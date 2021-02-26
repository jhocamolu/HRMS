using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Controllers.Odata
{

    public class SexosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SexosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/Sexos
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<Sexo>> Get()
        {
            return contexto.Sexos;
        }

        // GET: api/Sexos/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public async Task<ActionResult<Sexo>> Get([FromODataUri] int key)
        {
            var genero = await contexto.Sexos.FindAsync(key);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }
    }
}
