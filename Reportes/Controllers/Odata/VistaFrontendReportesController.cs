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
using Reportes.Models.NoMapeado;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace Reportes.Controllers.Odata
{
    public class VistaFrontendReportesController : ControllerBase
    {
        private readonly ReportesDbContext contexto;

        public VistaFrontendReportesController(ReportesDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Reportes
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<VistaFrontendReporte> Get()
        {
            return this.contexto.Reportes.Select(p => new VistaFrontendReporte
            {
                Alias = p.Alias,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                VistaGeneracion = p.VistaGeneracion,
                EsModal = p.EsModal,
                Extension = p.Extension,
                SubcategoriaId = p.SubcategoriaId,
                Subcategoria = p.Subcategoria
            }
            );
        }

        // GET: odata/Reportes/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<VistaFrontendReporte> Get([FromODataUri] string key)
        {
            IQueryable<VistaFrontendReporte> result = this.contexto.Reportes.Where(p => p.Alias == key).Select(p => new VistaFrontendReporte {
                Alias  = p.Alias,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                VistaGeneracion = p.VistaGeneracion,
                EsModal = p.EsModal,
                Extension = p.Extension,
                SubcategoriaId = p.SubcategoriaId,
                Subcategoria = p.Subcategoria
            }
            );
            return SingleResult.Create(result);
        }

    }
}
