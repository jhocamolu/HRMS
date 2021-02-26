using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class SolicitudVacacionesInterrupcionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SolicitudVacacionesInterrupcionesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/SolicitudVacacionesInterrupciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacacionesInterrupciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<SolicitudVacacionesInterrupcion> Get()
        {
            return this.contexto.SolicitudVacacionesInterrupciones;
        }

        // GET: odata/SolicitudVacacionesInterrupciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SolicitudVacacionesInterrupciones_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<SolicitudVacacionesInterrupcion> Get([FromODataUri] int key)
        {
            IQueryable<SolicitudVacacionesInterrupcion> result = this.contexto.SolicitudVacacionesInterrupciones.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
