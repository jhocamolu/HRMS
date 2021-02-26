using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportes.Infraestructura.DbContexto;
using Reportes.Models;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace Reportes.Controllers.Rest
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ReportesDbContext _context;

        public ReportesController(ReportesDbContext context)
        {
            _context = context;
        }

        // GET: api/Reportes/alias
        [HttpGet("{alias}")]
        public async Task<ActionResult<Reporte>> BuscaAlias(string alias)
        {
            var reporte = await _context.Reportes.Include(x => x.Subcategoria).ThenInclude(x => x.Categoria).FirstOrDefaultAsync(x => x.Alias == alias);
            if (reporte == null)
            {
                return NotFound();
            }
            return reporte;
        }
    }
}
