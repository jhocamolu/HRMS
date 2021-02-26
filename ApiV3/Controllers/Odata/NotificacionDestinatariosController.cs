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
    public class NotificacionDestinatariosController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public NotificacionDestinatariosController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        //GET: odata/Notificaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarios_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<NotificacionDestinatario> Get()
        {
            return this.contexto.NotificacionDestinatarios;
        }


        //GET: odata/Notificaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.NotificacionDestinatarios_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<NotificacionDestinatario> Get([FromODataUri] int key)
        {
            IQueryable<NotificacionDestinatario> notificacionDestinatarios = this.contexto.NotificacionDestinatarios.Where(p => p.Id == key);
            return SingleResult.Create(notificacionDestinatarios);
        }
    }
}