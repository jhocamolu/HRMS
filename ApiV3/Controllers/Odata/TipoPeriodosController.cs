using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU043_TipoPerido
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class TipoPeriodosController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoPeriodosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/TipoPeriodos
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoPeriodos_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]

        public IQueryable<TipoPeriodo> Get()
        {
            return this.contexto.TipoPeriodos;
        }


        // GET: odata/TipoPeriodos/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoPeriodos_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<TipoPeriodo> Get([FromODataUri] int key)
        {
            IQueryable<TipoPeriodo> resultado = this.contexto.TipoPeriodos.Where(p => p.Id == key);
            return SingleResult.Create(resultado);
        }
    }
}
