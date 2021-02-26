using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU052_registrar_informacion_básica_liquidación_nomina
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class NominasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public NominasController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/Nominas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public ActionResult<IQueryable<Nomina>> Get()
        {
            return this.contexto.Nominas;
        }

        // GET: odata/Nominas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Nominas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 30)]
        public SingleResult<Nomina> Get([FromODataUri] int key)
        {
            IQueryable<Nomina> result = this.contexto.Nominas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
