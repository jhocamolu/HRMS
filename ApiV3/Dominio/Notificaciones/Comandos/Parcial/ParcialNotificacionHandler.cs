using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Notificaciones.Comandos.Parcial
{
    public class ParcialNotificacionHandler : IRequestHandler<ParcialNotificacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialNotificacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialNotificacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Notificacion notificacion = await this.contexto.Notificaciones.FindAsync(request.Id);

                if (request.Tipo != null) notificacion.Tipo = (TipoNotificacion)request.Tipo;
                if (request.Titulo != null) notificacion.Titulo = request.Titulo;
                if (request.Mensaje != null) notificacion.Mensaje = request.Mensaje;
                if (request.EnEjecucion != null) notificacion.EnEjecucion = (bool)request.EnEjecucion;

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
