using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class FuncionariosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public FuncionariosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Funcionario
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Funcionarios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Funcionario> Get()
        {
            return this.contexto.Funcionarios;
        }


        //GET: odata/Funcionario/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Funcionarios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Funcionario> Get([FromODataUri] int key)
        {
            IQueryable<Funcionario> result = this.contexto.Funcionarios.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
