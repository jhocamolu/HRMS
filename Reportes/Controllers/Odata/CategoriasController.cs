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
    public class CategoriasController : ControllerBase
    {
        private readonly ReportesDbContext contexto;

        public CategoriasController(ReportesDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Categoria
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Categoria> Get()
        {
            return this.contexto.Categorias;
        }

        // GET: odata/Categoria/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Categoria> Get([FromODataUri] int key)
        {
            IQueryable<Categoria> result = this.contexto.Categorias.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
