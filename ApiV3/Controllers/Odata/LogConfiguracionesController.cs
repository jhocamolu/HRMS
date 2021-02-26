using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// @author Laura Katherine Estrada Arango
/// @email  desarrollador3@alcanosesp.com
/// @Description  Controlador obtiene los recursos de LogConfiguracion
/// Controlador Odata para busqueda personalizada

namespace ApiV3.Controllers.Odata
{
    public class LogConfiguracionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;

        public LogConfiguracionesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        // GET: odata/LogConfiguraciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LogConfiguraciones_Listar })]
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<_LogConfiguracion>>> Get()
        {
            return await this.contexto._LogConfiguraciones.ToListAsync();
        }

        // GET: odata/LogConfiguraciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.LogConfiguraciones_Obtener })]
        [HttpGet]
        [EnableQuery]
        public SingleResult<_LogConfiguracion> Get([FromODataUri] int key)
        {
            IQueryable<_LogConfiguracion> result = this.contexto._LogConfiguraciones.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
