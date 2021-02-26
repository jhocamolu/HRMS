using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{

    public class TipoHoraExtrasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoHoraExtrasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/TipoHoraExtras
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoHoraExtras_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<TipoHoraExtra> Get()
        {
            return contexto.TipoHoraExtras;
        }

        // GET: odata/TipoHoraExtras/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoHoraExtras_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<TipoHoraExtra> Get([FromODataUri] int key)
        {
            IQueryable<TipoHoraExtra> tipoHoraExtra = this.contexto.TipoHoraExtras.Where(p => p.Id == key);
            return SingleResult.Create(tipoHoraExtra);

        }

    }
}
