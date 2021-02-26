using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Reportes.Infraestructura.DbContexto;
using Reportes.Models;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace Reportes.Controllers.Odata
{
    public class ReportesController : ControllerBase
    {
        private readonly ReportesDbContext contexto;

        public ReportesController(ReportesDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Reportes
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Reporte> Get()
        {
            return this.contexto.Reportes;
        }

        // GET: odata/Reportes/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Reporte> Get([FromODataUri] int key)
        {
            IQueryable<Reporte> result = this.contexto.Reportes.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
