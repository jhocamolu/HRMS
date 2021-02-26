using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  HU025_Cargo_Dependencia

namespace ApiV3.Controllers.Odata
{
    public class CargoDependenciasController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public CargoDependenciasController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/CargoDependencias
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoDependencias_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public IQueryable<CargoDependencia> Get()
        {
            return this.contexto.CargoDependencias;
        }

        //GET: odata/CargoDependencias/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.CargoDependencias_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 10)]
        public SingleResult<CargoDependencia> Get([FromODataUri] int key)
        {
            IQueryable<CargoDependencia> result = this.contexto.CargoDependencias.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}