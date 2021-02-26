using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU043_SubPerido
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class SubPeriodosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public SubPeriodosController(NominaDbContext context)
        {
            contexto = context;
        }

        // GET: odata/SubPeriodos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SubPeriodos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public IQueryable<SubPeriodo> Get()
        {
            return this.contexto.SubPeriodos;
        }


        // GET: odata/SubPeriodos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.SubPeriodos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<SubPeriodo> Get([FromODataUri] int key)
        {
            IQueryable<SubPeriodo> resultado = this.contexto.SubPeriodos.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
