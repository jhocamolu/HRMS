using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class CentroCostosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public CentroCostosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/CentroCostos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroCostos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<CentroCosto> Get()
        {
            return this.contexto.CentroCostos;
        }


        //GET: odata/CentroCostos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroCostos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<CentroCosto> Get([FromODataUri] int key)
        {
            IQueryable<CentroCosto> resultado = this.contexto.CentroCostos.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
