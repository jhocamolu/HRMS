using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Notificaciones.Comandos.Crear
{
    public class CrearNotificacionHandler : IRequestHandler<CrearNotificacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearNotificacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(CrearNotificacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Notificacion notificacion = new Notificacion();

                notificacion.Tipo = (TipoNotificacion)request.Tipo;
                notificacion.Fecha = DateTime.Today;
                notificacion.Titulo = request.Titulo;
                notificacion.Mensaje = request.Mensaje;
                notificacion.EnEjecucion = false;


                this.contexto.Notificaciones.Add(notificacion);
                this.contexto.SaveChanges();

                if (request.NotificacionDestinatarios != null)
                {
                    foreach (var item in request.NotificacionDestinatarios)
                    {
                        NotificacionDestinatario notificacionDestinatario = new NotificacionDestinatario();

                        notificacionDestinatario.NotificacionId = (int)notificacion.Id;
                        if (item.FuncionarioId != null)
                        {
                            notificacionDestinatario.FuncionarioId = (int)item.FuncionarioId;
                        }
                        if (item.CorreoElectronico != null)
                        {
                            notificacionDestinatario.CorreoElectronico = item.CorreoElectronico;
                        }
                        notificacionDestinatario.Estado = EstadoNotificacion.Pendiente;


                        this.contexto.NotificacionDestinatarios.Add(notificacionDestinatario);
                        await this.contexto.SaveChangesAsync();
                    }
                }

                return CommandResult.Success(notificacion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
