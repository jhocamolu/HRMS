using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class NaturalezaJuridicasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NaturalezaJuridicasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/NaturalezaJuridicas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NaturalezaJuridicas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<NaturalezaJuridica>> Get()
        {
            return this.contexto.NaturalezaJuridicas;
        }

        // GET: odata/NaturalezaJuridicas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NaturalezaJuridicas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<NaturalezaJuridica> Get([FromODataUri] int key)
        {
            IQueryable<NaturalezaJuridica> result = this.contexto.NaturalezaJuridicas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
