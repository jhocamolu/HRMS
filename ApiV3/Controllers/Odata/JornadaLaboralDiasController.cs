using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class JornadaLaboralDiasController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public JornadaLaboralDiasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/JornadaLaboralDias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaboralDias_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<JornadaLaboralDia>> Get()
        {
            return this.contexto.JornadaLaboralDias;
        }

        //GET: odata/JornadaLaboralDias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.JornadaLaboralDias_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<JornadaLaboralDia> Get([FromODataUri] int key)
        {
            IQueryable<JornadaLaboralDia> jornadaLaboralDias = this.contexto.JornadaLaboralDias.Where(p => p.Id == key);
            return SingleResult.Create(jornadaLaboralDias);
        }
    }
}
