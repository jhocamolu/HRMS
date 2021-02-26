using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Eliminar
{
    public class EliminarSolicitudCesantiaHandler : IRequestHandler<EliminarSolicitudCesantiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarSolicitudCesantiaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarSolicitudCesantiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SolicitudCesantia solicitudCesantias = await contexto.SolicitudCesantias.FindAsync(request.Id);
                if (solicitudCesantias == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.SolicitudCesantias.Remove(solicitudCesantias);
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
