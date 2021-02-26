using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class InformacionBasicasController : ControllerBase
    {


        private readonly NominaDbContext context;
        public InformacionBasicasController(NominaDbContext context)
        {
            this.context = context;
        }


        // GET: odata/InformacionBasicas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.InformacionBasicas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<InformacionBasica> Get()
        {
            return this.context.InformacionBasicas;
        }


        // GET: odata/InformacionBasicas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.InformacionBasicas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<InformacionBasica> Get([FromODataUri] int key)
        {
            IQueryable<InformacionBasica> informacionBasica = this.context.InformacionBasicas.Where(x => x.Id == key);
            return SingleResult.Create(informacionBasica);
        }
    }
}
