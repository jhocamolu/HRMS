using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
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
    public class NotificacionesController : ControllerBase
    {
        private readonly NominaDbContext contexto;
        public NotificacionesController(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        //GET: odata/Notificaciones
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Notificaciones_Listar })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public IQueryable<Notificacion> Get()
        {
            return this.contexto.Notificaciones.Select(n => new Notificacion
            {
                Id = n.Id,
                Tipo = n.Tipo,
                Fecha = n.Fecha,
                Titulo = n.Titulo,
                Mensaje = n.Mensaje,
                EnEjecucion = n.EnEjecucion,
                Pendiente = contexto.NotificacionDestinatarios.Where(x => x.NotificacionId == n.Id && x.Estado == EstadoNotificacion.Pendiente).Count(),
                Enviado = contexto.NotificacionDestinatarios.Where(x => x.NotificacionId == n.Id && x.Estado == EstadoNotificacion.Enviado).Count(),
                Fallido = contexto.NotificacionDestinatarios.Where(x => x.NotificacionId == n.Id && x.Estado == EstadoNotificacion.Fallido).Count(),
            });
        }

        //GET: odata/Notificaciones/5
        [TypeFilter(typeof(JwtValidationActionFilter), Arguments = new object[] { ConstantesPermisos.Notificaciones_Obtener })]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth = 50)]
        public SingleResult<Notificacion> Get([FromODataUri] int key)
        {
            IQueryable<Notificacion> notificacion = this.contexto.Notificaciones.Select(n => new Notificacion
            {
                Id = n.Id,
                Tipo = n.Tipo,
                Fecha = n.Fecha,
                Titulo = n.Titulo,
                Mensaje = n.Mensaje,
                EnEjecucion = n.EnEjecucion,
                Pendiente = contexto.NotificacionDestinatarios.Where(x => x.NotificacionId == n.Id && x.Estado == EstadoNotificacion.Pendiente).Count(),
                Enviado = contexto.NotificacionDestinatarios.Where(x => x.NotificacionId == n.Id && x.Estado == EstadoNotificacion.Enviado).Count(),
                Fallido = contexto.NotificacionDestinatarios.Where(x => x.NotificacionId == n.Id && x.Estado == EstadoNotificacion.Fallido).Count(),
            }).Where(p => p.Id == key);
            return SingleResult.Create(notificacion);
        }
    }
}