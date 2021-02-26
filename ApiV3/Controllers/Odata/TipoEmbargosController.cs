using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU051_Tipo_Embargos
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class TipoEmbargosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoEmbargosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/TipoEmbargos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public ActionResult<IQueryable<TipoEmbargo>> Get()
        {
            return this.contexto.TipoEmbargos;
        }

        // GET: api/TipoEmbargos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoEmbargos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<TipoEmbargo> Get([FromODataUri] int key)
        {
            IQueryable<TipoEmbargo> consulta = this.contexto.TipoEmbargos.Where(p => p.Id == key);
            return SingleResult.Create(consulta);
        }
    }
}
