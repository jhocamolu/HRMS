using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  Funcion Nominas
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class FuncionNominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public FuncionNominasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/FuncionNominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionNominas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IQueryable<FuncionNomina>> Get()
        {
            return this.contexto.FuncionNominas;
        }

        // GET: odata/FuncionNominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionNominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public SingleResult<FuncionNomina> Get([FromODataUri] int key)
        {
            IQueryable<FuncionNomina> result = this.contexto.FuncionNominas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
