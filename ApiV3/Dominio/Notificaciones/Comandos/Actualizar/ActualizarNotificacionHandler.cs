using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Notificaciones.Comandos.Actualizar
{
    public class ActualizarNotificacionHandler : IRequestHandler<ActualizarNotificacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarNotificacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarNotificacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Notificacion notificacion = await this.contexto.Notificaciones.FindAsync(request.Id);

                notificacion.Tipo = (TipoNotificacion)request.Tipo;
                notificacion.Titulo = request.Titulo;
                notificacion.Mensaje = request.Mensaje;
                notificacion.EnEjecucion = request.EnEjecucion;

                this.contexto.Notificaciones.Update(notificacion);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(notificacion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
