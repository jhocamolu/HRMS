using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class TercerosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TercerosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Tercero
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Terceros_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Tercero> Get()
        {
            return this.contexto.Terceros;
        }


        //GET: odata/Tercero/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Terceros_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Tercero> Get([FromODataUri] int key)
        {
            IQueryable<Tercero> result = this.contexto.Terceros.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
