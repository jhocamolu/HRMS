using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
/// @author Jhonatan Camilo Moreno Luna
/// @email  desarrollador1@alcanosesp.com
/// @Description HU053 Firmas de representante empresa
namespace ApiV3.Controllers.Odata
{
    public class RepresentanteEmpresasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public RepresentanteEmpresasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: api/RepresentanteEmpresas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RepresentanteEmpresas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public ActionResult<IEnumerable<RepresentanteEmpresa>> Get()
        {
            return contexto.RepresentanteEmpresas;
        }

        // GET: api/RepresentanteEmpresas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.RepresentanteEmpresas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<RepresentanteEmpresa> Get([FromODataUri] int key)
        {
            IQueryable<RepresentanteEmpresa> resultado = this.contexto.RepresentanteEmpresas.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }

    }
}
