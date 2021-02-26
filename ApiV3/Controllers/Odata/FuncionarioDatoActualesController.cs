using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class FuncionarioDatoActualesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public FuncionarioDatoActualesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/FuncionarioDatoActuales
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioDatoActuales_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<FuncionarioDatoActual> Get()
        {
            return this.contexto.FuncionarioDatoActuales;


        }

        // GET: odata/FuncionarioDatoActuales/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.FuncionarioDatoActuales_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<FuncionarioDatoActual> Get([FromODataUri] int key)
        {
            IQueryable<FuncionarioDatoActual> funcionarioDatos = this.contexto.FuncionarioDatoActuales.Where(p => p.Id == key);
            return SingleResult.Create(funcionarioDatos);
        }
    }
}
