using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU090_AdmininstrarCategoriaNovedad

namespace ApiV3.Controllers.Odata
{

    public class CategoriaNovedadesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CategoriaNovedadesController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/CategoriaNovedades
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaNovedades_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public IQueryable<CategoriaNovedad> Get()
        {
            return this.contexto.CategoriaNovedades;
        }

        // GET: odata/CategoriaNovedades/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CategoriaNovedades_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<CategoriaNovedad> Get([FromODataUri] int key)
        {
            IQueryable<CategoriaNovedad> result = this.contexto.CategoriaNovedades.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
