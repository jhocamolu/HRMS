using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU025_Administrar_Cargos
/// Controlador Odata para busqueda personalizada
namespace ApiV3.Controllers.Odata
{

    public class GruposController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public GruposController(NominaDbContext context)
        {
            contexto = context;
        }


        // GET: odata/Grupos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Grupos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Grupo> Get()
        {
            return this.contexto.Grupos;
        }

        // GET: odata/Grupos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Grupos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<Grupo> Get([FromODataUri] int key)
        {
            IQueryable<Grupo> result = this.contexto.Grupos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

    }
}
