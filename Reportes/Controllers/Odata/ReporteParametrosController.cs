using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportes.Infraestructura.DbContexto;
using Reportes.Models;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace Reportes.Controllers.Odata
{
    public class ReporteParametrosController : ControllerBase
    {
        private readonly ReportesDbContext contexto;

        public ReporteParametrosController(ReportesDbContext context)
        {
            contexto = context;
        }

        // GET: odata/ReporteParametros
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<ReporteParametro> Get()
        {
            return this.contexto.ReporteParametros;
        }

        // GET: odata/ReporteParametros/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<ReporteParametro> Get([FromODataUri] int key)
        {
            IQueryable<ReporteParametro> result = this.contexto.ReporteParametros.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
