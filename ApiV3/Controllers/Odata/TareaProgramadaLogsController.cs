using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiV3.Controllers.Odata
{
    public class TareaProgramadaLogsController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public TareaProgramadaLogsController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/TareaProgramadaLogs
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadaLogs_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<TareaProgramadaLog> Get()
        {
            return this.contexto.TareasProgramadasLogs;
        }

        //GET: odata/TareaProgramadaLogs/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.TareaProgramadaLogs_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<TareaProgramadaLog> Get([FromODataUri] int key)
        {
            IQueryable<TareaProgramadaLog> tareasProgramadasLog = this.contexto.TareasProgramadasLogs.Where(p => p.Id == key);
            return SingleResult.Create(tareasProgramadasLog);
        }
    }
}