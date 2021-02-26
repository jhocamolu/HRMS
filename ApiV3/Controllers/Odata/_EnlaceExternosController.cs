using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
namespace ApiV3.Controllers.Odata
{
    public class _EnlaceExternosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public _EnlaceExternosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/_EnlaceExternos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos._EnlaceExternos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<_EnlaceExterno> Get()
        {
            return this.contexto._EnlaceExternos;
        }

        // GET: api/_EnlaceExternos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos._EnlaceExternos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<_EnlaceExterno> Get([FromODataUri] int key)
        {
            IQueryable<_EnlaceExterno> result = this.contexto._EnlaceExternos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
