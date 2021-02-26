using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R.
/// @email  desarrollador5@alcanosesp.com
/// @Description  HU095_Reclutamiento_Selección_Personal

namespace ApiV3.Controllers.Odata
{
    public class CandidatosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CandidatosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/andidatos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Candidatos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<Candidato>> Get()
        {
            return contexto.Candidatos;
        }

        // GET: api/andidatos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Candidatos_Obtener })]
        [HttpGet()]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Candidato> Get([FromODataUri] int key)
        {
            IQueryable<Candidato> result = this.contexto.Candidatos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
