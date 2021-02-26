using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Notificaciones.Comandos.Eliminar
{
    public class EliminarNotificacionHandler : IRequestHandler<EliminarNotificacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarNotificacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNotificacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Notificacion notificacion = await contexto.Notificaciones.FindAsync(request.Id);
                if (notificacion == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                contexto.Notificaciones.Remove(notificacion);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
