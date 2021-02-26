using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU098_Administrar años de trabajo

namespace ApiV3.Controllers.Odata
{

    public class AnnoVigenciasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public AnnoVigenciasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/AnnoVigencias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AnnoVigencias_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<AnnoVigencia>> Get()
        {
            return this.contexto.AnnoVigencias;
        }

        // GET: odata/AnnoVigencias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.AnnoVigencias_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<AnnoVigencia> Get([FromODataUri] int key)
        {
            IQueryable<AnnoVigencia> result = this.contexto.AnnoVigencias.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
