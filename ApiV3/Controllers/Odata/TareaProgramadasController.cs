using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class TareaProgramadasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TareaProgramadasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/TareaProgramadas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<TareaProgramada> Get()
        {
            return this.contexto.TareaProgramadas;
        }

        //GET: odata/TareaProgramadas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<TareaProgramada> Get([FromODataUri] int key)
        {
            IQueryable<TareaProgramada> tareaProgramada = this.contexto.TareaProgramadas.Where(p => p.Id == key);
            return SingleResult.Create(tareaProgramada);
        }
    }
}