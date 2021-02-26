using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU035_Tipo_Contratos

namespace ApiV3.Controllers.Odata
{
    public class TipoContratosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoContratosController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: odata/TipoContratos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoContratos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TipoContrato>> Get()
        {
            return contexto.TipoContratos;
        }

        // GET: odata/TipoContratos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoContratos_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<TipoContrato> Get(int key)
        {
            IQueryable<TipoContrato> result = this.contexto.TipoContratos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
