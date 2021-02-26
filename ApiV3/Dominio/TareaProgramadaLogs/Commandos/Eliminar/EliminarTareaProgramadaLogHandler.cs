using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TareaProgramadaLogs.Commandos.Eliminar
{
    public class EliminarTareaProgramadaLogHandler : IRequestHandler<EliminarTareaProgramadaLogRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTareaProgramadaLogHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTareaProgramadaLogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TareaProgramadaLog tareaProgramadaLog = await this.contexto.TareasProgramadasLogs.FindAsync(request.Id);
                if (tareaProgramadaLog == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TareasProgramadasLogs.Remove(tareaProgramadaLog);
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
