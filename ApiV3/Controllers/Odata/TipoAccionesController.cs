using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class TipoAccionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TipoAccionesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/TipoAcciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAcciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<TipoAccion> Get()
        {
            return this.contexto.TipoAcciones;
        }


        //GET: odata/TipoAcciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoAcciones_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<TipoAccion> Get([FromODataUri] int key)
        {
            IQueryable<TipoAccion> result = this.contexto.TipoAcciones.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}