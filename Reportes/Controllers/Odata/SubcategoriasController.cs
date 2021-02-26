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
 
    public class SubcategoriasController : ControllerBase
    {
        private readonly ReportesDbContext contexto;

        public SubcategoriasController(ReportesDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Subcategorias
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Subcategoria> Get()
        {
            return this.contexto.Subcategorias;
        }

        // GET: odata/Subcategorias/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Subcategoria> Get([FromODataUri] int key)
        {
            IQueryable<Subcategoria> result = this.contexto.Subcategorias.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
