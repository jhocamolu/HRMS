using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Filtros;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// @author Jesus Albeiro Gaviria R
/// @email  desarrollador5@alcanosesp.com
/// Controlador
/// Sprint8

namespace ApiV3.Controllers.Odata
{
    public class NotificacionDestinatarioLogsController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public NotificacionDestinatarioLogsController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Beneficios
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarioLogs_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<NotificacionDestinatarioLog> Get()
        {
            return this.contexto.NotificacionDestinatarioLogs;
        }


        //GET: odata/Beneficios/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarioLogs_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<NotificacionDestinatarioLog> Get([FromODataUri] int key)
        {
            IQueryable<NotificacionDestinatarioLog> notificacionDestinatariosLog = this.contexto.NotificacionDestinatarioLogs.Where(p => p.Id == key);
            return SingleResult.Create(notificacionDestinatariosLog);
        }
    }
}