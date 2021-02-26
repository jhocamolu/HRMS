using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description  HU002
namespace ApiV3.Controllers.Odata
{
    public class DivisionPoliticaNiveles2Controller : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public DivisionPoliticaNiveles2Controller(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/DivisionPoliticaNiveles2
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles2_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<DivisionPoliticaNivel2>> Get()
        {
            return contexto.DivisionPoliticaNiveles2;
        }

        // GET: api/DivisionPoliticaNiveles2/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles2_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<DivisionPoliticaNivel2> Get([FromODataUri] int key)
        {
            IQueryable<DivisionPoliticaNivel2> result = this.contexto.DivisionPoliticaNiveles2.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
