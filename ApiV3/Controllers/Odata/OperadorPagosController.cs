using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU019_Informacion_basica
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class OperadorPagosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public OperadorPagosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: api/OperadorPagos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.OperadorPagos_Listar })]
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<OperadorPago>> Get()
        {
            return contexto.OperadorPagos;
        }

        // GET: api/OperadorPagos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.OperadorPagos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<OperadorPago> Get([FromODataUri] int key)
        {
            IQueryable<OperadorPago> result = this.contexto.OperadorPagos.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
