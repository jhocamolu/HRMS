using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class ContratosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public ContratosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Contratos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Contratos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<Contrato> Get()
        {
            return this.contexto.Contratos;
        }


        //GET: odata/Contratos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Contratos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<Contrato> Get([FromODataUri] int key)
        {
            IQueryable<Contrato> resultado = this.contexto.Contratos.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
