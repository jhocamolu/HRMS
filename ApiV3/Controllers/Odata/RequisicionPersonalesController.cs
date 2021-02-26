using ApiV3.Infraestructura.DbContexto;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria Rubio
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU094_Administrar_Requisiciones_Personal
/// Controlador Odata para busqueda las Requisiciones de Personal.

namespace ApiV3.Controllers.Odata
{
    public class RequisicionPersonalesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public RequisicionPersonalesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/RequisicionPersonales
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<RequisicionPersonal>> Get()
        {
            return this.contexto.RequisicionPersonales;
        }

        //GET: odata/RequisicionPersonales/5
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<RequisicionPersonal> Get([FromODataUri] int key)
        {
            IQueryable<RequisicionPersonal> requisicionPersonales = this.contexto.RequisicionPersonales.Where(p => p.Id == key);
            return SingleResult.Create(requisicionPersonales);
        }
    }
}
