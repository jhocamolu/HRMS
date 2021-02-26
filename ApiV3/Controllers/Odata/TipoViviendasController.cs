using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description HU008_Administrar_Tipo_Viviendas
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{

    public class TipoViviendasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public TipoViviendasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/TipoViviendas
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoViviendas_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public ActionResult<IQueryable<TipoVivienda>> Get()
        {
            return this.contexto.TipoViviendas;
        }


        //GET: odata/TipoViviendas/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TipoViviendas_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]

        public SingleResult<TipoVivienda> Get([FromODataUri] int key)
        {
            IQueryable<TipoVivienda> result = this.contexto.TipoViviendas.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
