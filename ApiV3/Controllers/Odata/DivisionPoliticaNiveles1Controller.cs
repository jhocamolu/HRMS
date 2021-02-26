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

    public class DivisionPoliticaNiveles1Controller : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public DivisionPoliticaNiveles1Controller(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles1_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<DivisionPoliticaNivel1>> Get()
        {
            return contexto.DivisionPoliticaNiveles1;
        }

        // GET: api/DivisionPoliticaNiveles2/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.DivisionPoliticaNiveles1_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<DivisionPoliticaNivel1> Get([FromODataUri] int key)
        {
            IQueryable<DivisionPoliticaNivel1> result = this.contexto.DivisionPoliticaNiveles1.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
