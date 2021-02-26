using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU069_AplicacionExterna
/// 
namespace ApiV3.Controllers.Odata
{

    public class AplicacionExternasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public AplicacionExternasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/AplicacionExternas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<AplicacionExterna> Get()
        {
            return this.contexto.AplicacionExternas;
        }


        // GET: odata/AplicacionExternas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AplicacionExternas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<AplicacionExterna> Get([FromODataUri] int key)
        {
            IQueryable<AplicacionExterna> result = this.contexto.AplicacionExternas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
