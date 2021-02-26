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
    public class NominaFuncionariosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NominaFuncionariosController(NominaDbContext context)
        {
            this.contexto = context;
        }

        // GET: api/NominaFuncionarios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public ActionResult<IQueryable<NominaFuncionario>> Get()
        {
            return contexto.NominaFuncionarios;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NominaFuncionarios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<NominaFuncionario> Get([FromODataUri] int key)
        {
            IQueryable<NominaFuncionario> result = this.contexto.NominaFuncionarios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
