using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class CentroOperativosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public CentroOperativosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/CentroOperativos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroOperativos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<CentroOperativo> Get()
        {
            return this.contexto.CentroOperativos;
        }


        //GET: odata/CentroOperativos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CentroOperativos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<CentroOperativo> Get([FromODataUri] int key)
        {
            IQueryable<CentroOperativo> resultado = this.contexto.CentroOperativos.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
