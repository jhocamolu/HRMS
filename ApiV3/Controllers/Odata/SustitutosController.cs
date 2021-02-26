using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU068_Sustituto
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class SustitutosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SustitutosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/Sustitutos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Sustitutos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Sustituto> Get()
        {
            return this.contexto.Sustitutos;
        }

        // GET: odata/Sustitutos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Sustitutos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Sustituto> Get([FromODataUri] int key)
        {
            IQueryable<Sustituto> query = this.contexto.Sustitutos.Where(p => p.Id == key);
            return SingleResult.Create(query);
        }
    }
}
