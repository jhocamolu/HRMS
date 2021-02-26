using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU028
namespace ApiV3.Controllers.Odata
{
    public class FuncionarioEstudiosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public FuncionarioEstudiosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/FuncionarioEstudios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioEstudios_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<FuncionarioEstudio>> Get()
        {
            return contexto.FuncionarioEstudios;
        }

        // GET: odata/FuncionarioEstudios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioEstudios_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<FuncionarioEstudio> Get([FromODataUri] int key)
        {
            IQueryable<FuncionarioEstudio> result = this.contexto.FuncionarioEstudios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
