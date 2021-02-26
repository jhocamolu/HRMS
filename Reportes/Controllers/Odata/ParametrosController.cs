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
    public class ParametrosController : ControllerBase
    {
        private readonly ReportesDbContext contexto;

        public ParametrosController(ReportesDbContext context)
        {
            contexto = context;
        }
        // GET: odata/Parametros
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Parametro> Get()
        {
            return this.contexto.Parametros;
        }

        // GET: odata/Parametros/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Parametro> Get([FromODataUri] int key)
        {
            IQueryable<Parametro> result = this.contexto.Parametros.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
