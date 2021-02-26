using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU044_Grupos_Nomina
/// Controlador Odata para busqueda personalizada
/// 
namespace ApiV3.Controllers.Odata
{
    public class GrupoNominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public GrupoNominasController(NominaDbContext context)
        {
            contexto = context;
        }

        public object GrupoNominas { get; private set; }

        // GET: odata/GrupoNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GrupoNominas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<GrupoNomina> Get()
        {
            return this.contexto.GrupoNominas;
        }

        // GET: odata/GrupoNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.GrupoNominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<GrupoNomina> Get([FromODataUri] int key)
        {
            IQueryable<GrupoNomina> result = this.contexto.GrupoNominas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
